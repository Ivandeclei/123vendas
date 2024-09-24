namespace _123Vendas.Domain.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public long UnitValue { get; set; }
        public int Discount { get; set; }
        public long TotalItemValue { get; set; }
        public StatusSaleEnum statusItem { get; set; }
    }
}
