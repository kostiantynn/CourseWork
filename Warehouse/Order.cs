using System;
using System.Collections;
using System.Collections.Generic;
using Exceptions;

namespace Warehouse
{
    public class Order : Warehouse, IWarehouse, IEnumerable
    {
        
        public Order()
        {
            _products = new List<Product>();
        }

        public void AddProductsToTheOrder()
        {
            Console.Write("How many products do you want to choose? - ");
            var order = Console.ReadLine();
            if (order != null && order.Split('.').Length > 1)
            {
                throw new FormatException("Floating point number is restricted.");
            }
            var number = Convert.ToInt32(order) + 1;
            if (number < 0)
            {
                throw new NegativeNumberException("Input number is negative.");
            }
            for (int i = 1; i < number; i++)
            {
                Console.Write($"Write your {i} product name and quantity: ");
                var temp = Console.ReadLine()?.Split(' ');
                if (temp != null && temp.Length == 2)
                {
                    if (Convert.ToInt32(temp[1]) < 0)
                    {
                        throw new NegativeNumberException("Input number is negative.");
                    }
                    AddProduct(new Product(temp[0], Convert.ToInt32(temp[1])));
                }
                else
                {
                    throw new NullReferenceException("You are trying to add null to list");
                }
            }

            Console.WriteLine("Products you've ordered:");
            ShowExistingProducts();
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }
        public IEnumerator GetEnumerator()
        {
            return _products.GetEnumerator();
        }

        public bool IsEmpty()
        {
            return _products.Count == 0;
        }
    }
}