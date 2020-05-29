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
        public void ShowExistingProducts()
        {
            if (IsEmpty())
            {
                throw new UnderflowException("Your order is empty, order something or leave.");
            }
            foreach (var product in _products)
                Console.WriteLine(product);
        }

        public IEnumerator GetEnumerator()
        {
            return _products.GetEnumerator();
        }

        public void AddProduct(Product product)
        {
            _products += product;
        }

        public bool IsEmpty()
        {
            return _products.Count == 0;
        }

        public void AddProductsToTheOrder(Store store)
        {
            Console.Write("How many products do you want to choose? - ");
            var order = Console.ReadLine();
            if (order?.Split('.').Length > 1) throw new FormatException("Floating point number is restricted.");
            var number = Convert.ToInt32(order) + 1;
            if (number < 0) throw new NegativeNumberException("Input number is negative.");
            for (var i = 1; i < number; i++)
            {
                Console.Write($"Write your {i} product name:");
                var name = Console.ReadLine();
                if (name?.Length < 1) throw new ArgumentException("You didn't write name of the product.");
                Console.Write($"Write your {i} product quantity:");
                var quantity = int.Parse(Console.ReadLine() ?? throw new NullReferenceException());
                if (Convert.ToInt32(quantity) < 0) throw new NegativeNumberException("Input number is negative.");
                var newProduct = new Product(name, quantity);
                if (!store.IsInWarehouse(newProduct) || store.IsInWarehouse(newProduct) && !store.EnoughQuantity(newProduct))
                {
                    store.AddProduct(newProduct);
                }
                AddProduct(newProduct);
            }

            Console.WriteLine("Products you've ordered:");
            ShowExistingProducts();
        }
    }
}