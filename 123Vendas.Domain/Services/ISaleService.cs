using _123Vendas.Domain.Models;

namespace _123Vendas.Domain.Services
{
    public interface ISaleService
    {
        Task SaveSaleAsync(Sale sale);
        Task UpdateSaleAsync(Sale sale);
        Task<IEnumerable<Sale>> GetAllSaleAsync();
        Task<Sale> GetSaleAsync(Guid id);
        Task DeleteSaleAsync(Guid id);
    }
}
