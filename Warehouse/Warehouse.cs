using System;
using System.Collections.Generic;
using System.Linq;

namespace Warehouse
{
    public abstract class Warehouse
    {
        public delegate void ProductHandler(object sender, StoreHandlerArgs handlerArgs);

        protected List<Product> _products;

        public bool IsInWarehouse(Product product)
        {
            return _products.Any(item => item.Name == product.Name);
        }

        public virtual void ShowExistingProducts()
        {
            foreach (var product in _products) Console.WriteLine(product);
        }
    }
}