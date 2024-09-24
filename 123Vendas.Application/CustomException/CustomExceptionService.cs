namespace _123Vendas.Application.CustomException
{
    public class CustomExceptionService : Exception
    {
        public CustomExceptionService(string message) : base(message)
        {
        }

        public override string ToString()
        {
            return Message;
        }
    }
}
