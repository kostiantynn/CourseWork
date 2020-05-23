namespace Warehouse
{
    public class StoreHandlerArgs
    {
        public StoreHandlerArgs(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}