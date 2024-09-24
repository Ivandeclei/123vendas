using _123Vendas.Domain.Models;

namespace _123Vendas.Domain.Adapters
{
    public interface IPublishQueue
    {
        Task publishAsync(MessageQueue messageQueue);
    }
}
