using System;
using System.Collections.Generic;
using Exceptions;
using Warehouse;

namespace ConsoleApp
{
    internal static class Program
    {
        public static string menu = "Choose what you will do:\n" +
                                    "0 - Exit program\n" +
                                    "1 - Add new products to the order\n" +
                                    "2 - Show existing products in warehouse\n" +
                                    "3 - Show my order\n" +
                                    "4 - Take order and leave";

        private static void Main()
        {
            var products = new List<Product>
            {
                new Product("apple", 10),
                new Product("cherry", 20),
                new Product("blueberry", 40),
                new Product("pineapple", 25)
            };
            var store = new Store(products);
            store.ProductAction += StoreEvent;
            TakeNewOrder(store);
        }

        private static void TakeNewOrder(Store store)
        {
            var order = new Order();
            var active = true;
            while (active)
                try
                {
                    Console.WriteLine(menu);
                    var input = Convert.ToInt32(Console.ReadLine());
                    switch (input)
                    {
                        case 0:
                            active = false;
                            break;
                        case 1:
                            order.AddProductsToTheOrder();
                            TakeOrderFromStore(order, store);
                            break;
                        case 2:
                            store.ShowExistingProducts();
                            break;
                        case 3:
                            order.ShowExistingProducts();
                            break;
                        case 4:
                            if (order.IsEmpty())
                            {
                                Console.WriteLine("You didn't make take any product from warehouse.");
                                break;
                            }

                            Console.WriteLine("Products that left in warehouse");
                            store.ShowExistingProducts();
                            var exitInput = int.Parse(Console.ReadLine() ?? throw new NullReferenceException());
                            Console.WriteLine("Do you want to make a new order?\n");
                            Console.Write("1 - take new order; 2 - Leave: ");
                            switch (exitInput)
                            {
                                case 1:
                                    TakeNewOrder(store);
                                    break;
                                case 2:
                                    Console.WriteLine("You can take your order and leave!!!");
                                    break;
                            }

                            active = false;
                            break;
                        default:
                            Console.WriteLine("You didn't choose anything, try again");
                            break;
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
                catch (UnderflowException e)
                {
                    Console.WriteLine(e.Message);
                }
        }

        private static void StoreEvent(object sender, StoreHandlerArgs handlerArgs)
        {
            Console.WriteLine(handlerArgs.Message);
        }

        private static void TakeOrderFromStore(Order order, Store store)
        {
            foreach (Product product in order)
                try
                {
                    store.TakeOrder(product);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    store.AddProduct(product);
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine(e.Message);
                    store.DeleteZeroProduct(product);
                }
        }
    }
}