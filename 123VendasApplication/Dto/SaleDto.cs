using _123Vendas.Domain.Models;

namespace _123VendasApplication.Dto
{
    public class SaleDto
    {
        public Guid SaleNumber { get; set; }
        public ClientDto? Client { get; set; }
        public BranchStoreDto? BranchStore { get; set; }
        public IEnumerable<ProductDto>? Products { get; set; }

        public StatusSaleEnum Status { get; set; }
    }
}
