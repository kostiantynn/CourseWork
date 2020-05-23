using System;
using System.Collections.Generic;

namespace Warehouse
{
    public class Product
    {
        private int _quantityOfProduct;
        public string Name { get; }

        public int QuantityOfProduct
        {
            get
            {
                if (_quantityOfProduct == 0)
                {
                    throw new NullReferenceException(
                        $"The product {Name} ended in warehouse, but you successfully taken last.");
                }

                return _quantityOfProduct;
            }
            private set
            {
                if (value == 0)
                {
                    throw new NullReferenceException(
                        $"The product {Name} ended in warehouse, but you successfully taken last.");
                }

                _quantityOfProduct = value;
            }
        }

        public Product(string name, int quantityOfProduct)
        {
            Name = name;
            _quantityOfProduct = quantityOfProduct;
        }

        public static List<Product> operator +(List<Product> products, Product product)
        {
            try
            {
                var elementIndex = products.IndexOf(products.Find(item => item.Name == product.Name));
                products[elementIndex].QuantityOfProduct += product.QuantityOfProduct;
            }
            catch (Exception)
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

        public override string ToString()
        {
            return $"{Name} -- {QuantityOfProduct}";
        }
    }
}