using _123Vendas.DbAdapter.DbAdapterConfiguration;
using _123Vendas.Domain.Adapters;
using _123Vendas.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace _123Vendas.DbAdapter
{
    public class SaleWriteAdapter : ISaleWriteAdapter
    {
        private readonly Context _context;
        private DbSet<Sale> _sale;
        public SaleWriteAdapter(Context context)
        {
            _context = context;
            _sale = _context.Set<Sale>();
        }
        public async Task DeleteAsync(Guid id)
        {
            var sale = await _sale.FirstOrDefaultAsync(s => s.Id == id);

            if (sale != null)
            {
                _sale.Remove(sale);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Guid> SaveAsync(Sale entity)
        {
            _sale.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task UpdateAsync(Sale entity)
        {
            _sale.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
