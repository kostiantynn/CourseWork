namespace Warehouse
{
    public static class Constants
    {
        public static readonly string TakeAWhile = "It will take a while to deliver new products.";
        public static readonly string EmptyStore = "Store is empty, deliver new products before taking an order.";
        public static readonly string EmptyOrder = "Order is empty, deliver new products before taking an order.";
        public static readonly string DeliveryQueueListing = "Delivery order - listing of products:";
        public static readonly string StoreListing = "Store - listing of products:";
        public static readonly string OrderListing = "Order - listing of products:";
        public static readonly string EmptyQueue = "Delivery queue is empty.";
        public static readonly string DeliverSoon = "We are going to deliver it soon";
        public static readonly string DeliveredYet = "Product hasn't been delivered yet.";
        public static readonly int DeliveryTime = 15000;
    }
}