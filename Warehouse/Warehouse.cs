using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Warehouse
{
    public abstract class Warehouse : IEnumerable
    {
        public delegate void ProductHandler(object sender, StoreHandlerArgs handlerArgs);

        protected List<Product> _products;

        public IEnumerator GetEnumerator()
        {
            return _products.GetEnumerator();
        }

        public bool IsInWarehouse(string productName)
        {
            return _products.Any(item => item.Name == productName);
        }

        public virtual void ShowExistingProducts()
        {
            foreach (var product in _products) Console.WriteLine(product);
        }

        public bool IsEmpty()
        {
            return _products.Count == 0;
        }
    }
}