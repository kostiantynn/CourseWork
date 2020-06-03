namespace Warehouse
{
    internal interface IWarehouse
    {
        public void AddProduct(Product product);
        public void DeleteProduct(string productName);
    }
}