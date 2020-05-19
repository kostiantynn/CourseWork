using System.Collections.Generic;
using Exceptions;
using System;

namespace Warehouse
{
    public abstract class Warehouse
    {
        public List<Product> Products = new List<Product>();

        public void ShowExistingProducts(List<Product> products)
        {
            if (products.Count == 0)
                throw new UnderflowException("Warehouse is empty.");
            
            foreach (var product in products)
                Console.WriteLine($"{product.Name} -- {product.QuantityOfProduct}");
            
        }
    }
}