using System.Collections.Generic;
using Exceptions;

namespace Warehouse
{
    internal class DeliveryQueue : Warehouse, IWarehouse
    {
        internal DeliveryQueue()
        {
            _products = new List<Product>();
        }
        // Public interface methods implementation
        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public void DeleteProduct(string productName)
        {
            if (IsInWarehouse(productName)) _products.Remove(_products.Find(item => item.Name == productName));
        }
        // Virtual function overriding
        public override void ShowExistingProducts()
        {
            if (IsEmpty()) throw new UnderflowException(Constants.EmptyQueue);
            base.ShowExistingProducts();
        }
    }
}