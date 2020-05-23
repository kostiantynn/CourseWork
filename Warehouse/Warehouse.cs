using System;
using System.Collections.Generic;
using Exceptions;

namespace Warehouse
{
    public abstract class Warehouse
    {
        protected List<Product> _products;

        public void ShowExistingProducts()
        {
            if (_products.Count == 0)
                throw new UnderflowException("Warehouse is empty.");

            foreach (var product in _products)
                Console.WriteLine(product);
        }
    }
}