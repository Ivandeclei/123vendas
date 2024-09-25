namespace _123Vendas.Application.Constants
{
    public class CommonConstants
    {
        public const string CREATE = "CREATE";
        public const string UPDATE = "UPDATE";
        public const string DELETE = "DELETE";
        public const string COMMENT = "SELECT";

        //RoutingKeys
        public const string PURCHASE_CANCELED_ROUTING_KEY = "routingkey_purchase_canceled";
        public const string PURCHASE_MODIFIED_ROUTING_KEY = "routingkey_purchase_modified";
        public const string PURCHASE_CREATED_ROUTING_KEY = "routingkey_purchase_created";
        public const string ITEM_CANCELED_ROUTING_KEY = "routingkey_item_canceled";
    }
}
