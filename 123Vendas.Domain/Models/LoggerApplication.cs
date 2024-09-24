namespace _123Vendas.Domain.Models
{
    public class LoggerApplication
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public string? Message { get; set; }
        public string? Client { get; set; }
        public string? Action { get; set; }
        public Guid SaleNumber { get; set; }
        public Sale? Sale { get; set; }
    }
}
