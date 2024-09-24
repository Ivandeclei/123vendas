using _123Vendas.Application.Constants;
using _123Vendas.Application.CustomException;
using _123Vendas.Domain.Adapters;
using _123Vendas.Domain.Models;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace _123Vendas.Application.Tests
{
    public class SaleServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<ISaleReadAdapter> _saleReadAdapterMock;
        private readonly Mock<ISaleWriteAdapter> _saleWriteAdapterMock;
        private readonly Mock<IPublishQueue> _queueMock;
        private readonly Mock<ILogger<SaleService>> _loggerMock;
        private readonly SaleService _saleService; 

        public SaleServiceTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _saleReadAdapterMock = _fixture.Freeze<Mock<ISaleReadAdapter>>();
            _saleWriteAdapterMock = _fixture.Freeze<Mock<ISaleWriteAdapter>>();
            _queueMock = _fixture.Freeze<Mock<IPublishQueue>>();
            _loggerMock = _fixture.Freeze<Mock<ILogger<SaleService>>>();

            _saleService = new SaleService(_saleReadAdapterMock.Object, _saleWriteAdapterMock.Object, _queueMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task DeleteSaleAsync_WithEmptyId_ShouldThrowCustomException()
        {
            // Arrange
            var emptyId = Guid.Empty;

            // Act
            Func<Task> act = () => _saleService.DeleteSaleAsync(emptyId);

            // Assert
            await act.Should().ThrowAsync<CustomExceptionService>()
                .WithMessage(ExceptionMessages.REGISTER_IS_EMPTY);
        }

        [Fact]
        public async Task DeleteSaleAsync_WithNonExistentSale_ShouldThrowCustomException()
        {
            // Arrange
            var nonExistentSaleId = _fixture.Create<Guid>();
            _saleReadAdapterMock.Setup(x => x.GetByIdAsync(nonExistentSaleId)).ReturnsAsync((Sale)null);

            // Act
            Func<Task> act = () => _saleService.DeleteSaleAsync(nonExistentSaleId);

            // Assert
            await act.Should().ThrowAsync<CustomExceptionService>()
                .WithMessage(ExceptionMessages.REGISTER_NOT_FOUND);
        }

        [Fact]
        public async Task DeleteSaleAsync_WithValidId_ShouldCallDeleteAsync()
        {
            // Arrange
            var validSaleId = _fixture.Create<Guid>();
            var sale = _fixture.Create<Sale>();
            _saleReadAdapterMock.Setup(x => x.GetByIdAsync(validSaleId)).ReturnsAsync(sale);

            // Act
            await _saleService.DeleteSaleAsync(validSaleId);

            // Assert
            _saleWriteAdapterMock.Verify(x => x.DeleteAsync(validSaleId), Times.Once);
        }

        [Fact]
        public async Task GetAllSaleAsync_ShouldReturnListOfSales()
        {
            // Arrange
            var sales = _fixture.CreateMany<Sale>(5);
            _saleReadAdapterMock.Setup(x => x.GetAllAsync()).ReturnsAsync(sales);

            // Act
            var result = await _saleService.GetAllSaleAsync();

            // Assert
            result.Should().BeEquivalentTo(sales);
        }

        [Fact]
        public async Task GetSaleAsync_WithEmptyId_ShouldThrowCustomException()
        {
            // Arrange
            var emptyId = Guid.Empty;

            // Act
            Func<Task> act = () => _saleService.GetSaleAsync(emptyId);

            // Assert
            await act.Should().ThrowAsync<CustomExceptionService>()
                .WithMessage(ExceptionMessages.REGISTER_IS_EMPTY);
        }

        [Fact]
        public async Task GetSaleAsync_WithNonExistentSale_ShouldThrowCustomException()
        {
            // Arrange
            var nonExistentSaleId = _fixture.Create<Guid>();
            _saleReadAdapterMock.Setup(x => x.GetByIdAsync(nonExistentSaleId)).ReturnsAsync((Sale)null);

            // Act
            Func<Task> act = () => _saleService.GetSaleAsync(nonExistentSaleId);

            // Assert
            await act.Should().ThrowAsync<CustomExceptionService>()
                .WithMessage(ExceptionMessages.REGISTER_NOT_FOUND);
        }

        [Fact]
        public async Task GetSaleAsync_WithValidId_ShouldReturnSale()
        {
            // Arrange
            var sale = _fixture.Create<Sale>();
            _saleReadAdapterMock.Setup(x => x.GetByIdAsync(sale.Id)).ReturnsAsync(sale);

            // Act
            var result = await _saleService.GetSaleAsync(sale.Id);

            // Assert
            result.Should().BeEquivalentTo(sale);
        }

        [Fact]
        public async Task SaveSaleAsync_WithNullSale_ShouldThrowArgumentNullException()
        {
            // Act
            Func<Task> act = () => _saleService.SaveSaleAsync(null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task SaveSaleAsync_WithValidSale_ShouldCallSaveAsync()
        {
            // Arrange
            var sale = _fixture.Create<Sale>();

            // Act
            await _saleService.SaveSaleAsync(sale);

            // Assert
            _saleWriteAdapterMock.Verify(x => x.SaveAsync(sale), Times.Once);
        }

        [Fact]
        public async Task UpdateSaleAsync_WithNullSale_ShouldThrowArgumentNullException()
        {
            // Act
            Func<Task> act = () => _saleService.UpdateSaleAsync(null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateSaleAsync_WithValidSale_ShouldCallUpdateAsync()
        {
            // Arrange
            var sale = _fixture.Create<Sale>();

            // Act
            await _saleService.UpdateSaleAsync(sale);

            // Assert
            _saleWriteAdapterMock.Verify(x => x.UpdateAsync(sale), Times.Once);
        }

        [Fact]
        public async Task publishQueueAsync_ShouldPublishToQueue()
        {
            // Arrange
            var actionEvent = _fixture.Create<ActionEventEnum>();
            var sale = _fixture.Create<Sale>();

            _queueMock.Setup(x => x.publishAsync(It.IsAny<MessageQueue>()))
                .Returns(Task.CompletedTask);

            // Act
            await _saleService.SaveSaleAsync(sale);

            // Assert
            _queueMock.Verify(x => x.publishAsync(It.IsAny<MessageQueue>()), Times.Once);
        }

    }
}
