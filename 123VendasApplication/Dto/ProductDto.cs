using _123Vendas.Domain.Models;

namespace _123VendasApplication.Dto
{
    public class ProductDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public long UnitValue { get; set; }
        public int Discount { get; set; }
        public long TotalItemValue { get; set; }
        public StatusSaleEnum statusItem { get; set; }
    }
}
