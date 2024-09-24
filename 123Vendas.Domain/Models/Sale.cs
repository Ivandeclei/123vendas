namespace _123Vendas.Domain.Models
{
    public class Sale : EntityBase
    {
        public Guid SaleNumber { get; set; }
        public Client? Client { get; set; }
        public BranchStore? BranchStore { get; set; }
        public IEnumerable<Product>? Products { get; set; }
        
        public StatusSaleEnum Status { get; set; }
    }
}
