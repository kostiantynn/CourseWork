namespace Warehouse
{
    public interface IWarehouse
    {
        public void AddProduct(Product product);
        public bool IsEmpty();
    }
}