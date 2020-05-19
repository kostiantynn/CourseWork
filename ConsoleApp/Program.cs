using System;
using System.Collections.Generic;
using Warehouse;
using Exceptions;

namespace ConsoleApp
{
    static class Program
    {
        static void Main()
        {
            var products = new List<Product>
            {
                new Product("apple", 10),
                new Product("cherry", 20),
                new Product("blueberry", 40),
                new Product("pineapple", 25)
            };
            TakeNewOrder(products);
        }

        private static void TakeNewOrder(List<Product> products)
        {
            var store = new Store(products);
            store.ProductAction += ProductAddedToStoreEvent;
            try
            {
                var order = new Order();
                order.AddProductsToTheOrder();
                Console.WriteLine("Do you want to add something to your order before we continue?\n");
                Console.Write("1 - Add new Products, 2 - continue: ");
                var input = Convert.ToInt32(Console.ReadLine());
                if (input == 1)
                {
                    order.AddProductsToTheOrder();
                }
                foreach (var product in order.Products)
                {
                    try
                    {
                        store.TakeOrder(product);
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e.Message);
                        store.AddProduct(product.Name, product.QuantityOfProduct);
                    }
                    catch (NullReferenceException e)
                    {
                        Console.WriteLine(e.Message);
                        store.DeleteZeroProduct(product);
                    }
                }
                
                Console.WriteLine("Products that left in warehouse");
                store.ShowExistingProducts(store.Products);
                Console.WriteLine("Do you want to make a new order?\n");
                Console.Write("1 - take new order, 2 - Leave: ");
                var finishInput = Convert.ToInt32(Console.ReadLine());
                if (finishInput == 1)
                {
                    TakeNewOrder(store.Products);
                }
                else
                {
                    Console.WriteLine("You can take your order and leave!!!");
                }
            }
            catch (NegativeNumberException e)
            {
                Console.WriteLine(e.Message + " Please enter non-negative number in input sheet.");
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message + " Please enter a number you trying to add NaN");
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message + " If you want to order products please use only integer values.");
            }
        }
        
        private static void ProductAddedToStoreEvent(object sender, StoreHandlerArgs handlerArgs)
        {
            Console.WriteLine(handlerArgs.Message);
        }
    }
}