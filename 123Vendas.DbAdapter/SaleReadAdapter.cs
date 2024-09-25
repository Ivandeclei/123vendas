using _123Vendas.DbAdapter.DbAdapterConfiguration;
using _123Vendas.Domain.Adapters;
using _123Vendas.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace _123Vendas.DbAdapter
{
    public class SaleReadAdapter : ISaleReadAdapter
    {
        private readonly Context _context;
        private DbSet<Sale> _sale;
        public SaleReadAdapter(Context context)
        {
            _context = context;
            _sale = _context.Set<Sale>();
        }
        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _sale
                .Include(c => c.Client)
                .Include(p => p.Products)
                .Include(b => b.BranchStore).ToListAsync();
        }

        public async Task<Sale> GetByIdAsync(Guid id)
        {
            return await _sale
                .Include(c => c.Client)
                .Include(p => p.Products)
                .Include(b => b.BranchStore).SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
