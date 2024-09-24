namespace _123Vendas.Domain.Models
{
    public class MessageQueue
    {
        public ActionEventEnum Status { get; set; }
        public Sale Sale { get; set; }
    }
}
