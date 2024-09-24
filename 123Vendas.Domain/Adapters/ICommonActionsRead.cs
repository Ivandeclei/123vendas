namespace _123Vendas.Domain.Adapters
{
    public interface ICommonActionsRead<T>
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
