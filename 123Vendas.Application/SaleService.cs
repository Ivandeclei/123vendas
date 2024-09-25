using _123Vendas.Application.Constants;
using _123Vendas.Application.CustomException;
using _123Vendas.Domain.Adapters;
using _123Vendas.Domain.Models;
using _123Vendas.Domain.Services;
using Microsoft.Extensions.Logging;

namespace _123Vendas.Application
{
    public class SaleService : ISaleService
    {
        private readonly ISaleReadAdapter _saleReadAdapter;
        private readonly ISaleWriteAdapter _saleWriteAdapter;
        private readonly IPublishQueue _queue;
        private readonly ILogger<SaleService> _logger;
        public SaleService(ISaleReadAdapter saleReadAdapter, ISaleWriteAdapter saleWriteAdapter, IPublishQueue queue, ILogger<SaleService> logger)
        {
            _saleReadAdapter = saleReadAdapter;
            _saleWriteAdapter = saleWriteAdapter;
            _queue = queue;
            _logger = logger;                
        }
        public async Task DeleteSaleAsync(Guid id)
        {
            if(id == Guid.Empty)
            {
                throw new CustomExceptionService(ExceptionMessages.REGISTER_IS_EMPTY);
            }

            _logger.LogInformation("Start method " + nameof(ISaleReadAdapter.GetByIdAsync) + "with id " + id);

            try
            {
                var sale = await _saleReadAdapter.GetByIdAsync(id);

                if (sale == null)
                {
                    throw new CustomExceptionService(ExceptionMessages.REGISTER_NOT_FOUND);
                }

                await _saleWriteAdapter.DeleteAsync(id);
                var messageQueue = CreateObject(sale, ActionEventEnum.DeletedSale, CommonConstants.PURCHASE_CANCELED_ROUTING_KEY);
                publishQueueAsync(messageQueue);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw ex;
            }
            
        }

        public async Task<IEnumerable<Sale>> GetAllSaleAsync()
        {
            _logger.LogInformation("Start method " + nameof(ISaleReadAdapter.GetAllAsync));

            try
            {
                return await _saleReadAdapter.GetAllAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw ex;
            }
        }

        public async Task<Sale> GetSaleAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new CustomExceptionService(ExceptionMessages.REGISTER_IS_EMPTY);
            }

            _logger.LogInformation("Start method " + nameof(ISaleReadAdapter.GetByIdAsync) + "with id " + id);

            try
            {
                var sale = await _saleReadAdapter.GetByIdAsync(id);

                if (sale == null)
                {
                    throw new CustomExceptionService(ExceptionMessages.REGISTER_NOT_FOUND);
                }

                return sale;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw ex;
            }
        }

        public async Task SaveSaleAsync(Sale sale)
        {
            ValidateSale(sale);
            _logger.LogInformation("Start method " + nameof(ISaleWriteAdapter.SaveAsync) + "with " + sale);

            try
            {
                await _saleWriteAdapter.SaveAsync(sale);
                var messageQueue = CreateObject(sale, ActionEventEnum.CreatedSale, CommonConstants.PURCHASE_CREATED_ROUTING_KEY);
                publishQueueAsync(messageQueue);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw ex;
            }

        }

        public async Task UpdateSaleAsync(Sale sale)
        {
            ValidateSale(sale);
            _logger.LogInformation("Start method " + nameof(ISaleWriteAdapter.UpdateAsync) + "with " + sale);

            try
            {
                await _saleWriteAdapter.UpdateAsync(sale);
                var messageQueue = CreateObject(sale, ActionEventEnum.UpdatedSale, CommonConstants.PURCHASE_MODIFIED_ROUTING_KEY);
                publishQueueAsync(messageQueue);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw ex;
            }
        }
        private void ValidateSale(Sale sale)
        {
            if (sale is null)
            {
                throw new ArgumentNullException(nameof(Sale));
            }
        }

        private async void  publishQueueAsync(MessageQueue messageQueue)
        {
            _logger.LogInformation("Start method " + nameof(IPublishQueue.PublishAsync) + "with " + messageQueue);
            try
            {
                await _queue.PublishAsync(messageQueue);

            }
            catch (Exception ex )
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        private MessageQueue CreateObject( Sale sale, ActionEventEnum actionEvent, string routingKey)
        {
            return new MessageQueue
            {
                Sale = sale,
                Status = actionEvent,
                RoutingKey = routingKey
            };
        }
    }
}
