using System;
using System.Collections.Generic;
using Exceptions;

namespace Warehouse
{
    public class Product
    {
        private int _quantityOfProduct;

        public Product(string name, int quantityOfProduct)
        {
            Name = name;
            _quantityOfProduct = quantityOfProduct;
        }

        public string Name { get; }

        public int QuantityOfProduct
        {
            get
            {
                if (_quantityOfProduct == 0)
                    throw new UnderflowException(
                        $"The product \"{Name}\" ended in warehouse.");

                return _quantityOfProduct;
            }
            private set
            {
                if (value == 0)
                    throw new UnderflowException(
                        $"The product \"{Name}\" ended in warehouse.");

                _quantityOfProduct = value;
            }
        }

        // Operators overriding in purpose of simplifying process of adding/deleting products from list
        public static List<Product> operator +(List<Product> products, Product product)
        {
            try
            {
                var elementIndex = products.IndexOf(products.Find(item => item.Name == product.Name));
                products[elementIndex].QuantityOfProduct += product.QuantityOfProduct;
            }
            catch (ArgumentOutOfRangeException)
            {
                products.Add(product);
            }

            return products;
        }

        public static List<Product> operator +(Product product, List<Product> products)
        {
            return products + product;
        }

        public static List<Product> operator -(List<Product> products, Product product)
        {
            var indexOfProduct = products.IndexOf(products.Find(item => item.Name == product.Name));
            products[indexOfProduct].QuantityOfProduct -= product._quantityOfProduct;
            return products;
        }

        public static List<Product> operator -(Product product, List<Product> products)
        {
            return products - product;
        }
        // Overriding ToString in purpose to correctly represent string representation of Product object
        public override string ToString()
        {
            return $"{Name} -- {QuantityOfProduct}";
        }
    }
}