using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Warehouse
{
    public class Store : Warehouse, IWarehouse
    {
        
        public delegate void StoreHandler(object sender, StoreHandlerArgs handlerArgs);
        public event StoreHandler ProductAction;
        
        public Store(List<Product> products)
        {
            Products = products;
            ShowExistingProducts(Products);
        }

        public void AddProduct(string name, int quantityOfProduct)
        {
            Products += new Product(name, RandomValuation(quantityOfProduct));
            var orderedProduct = new Product(name, quantityOfProduct);
            ProductAction?.Invoke(orderedProduct, 
                new StoreHandlerArgs($"Product \"{orderedProduct.Name}\"" +
                                     " successfully order, wait until it will be delivered. " 
                                     + IWarehouse.TAKE_A_WHILE));
            DeliverTimer(3000);
            ProductAction?.Invoke(orderedProduct, 
                new StoreHandlerArgs($"Product \"{orderedProduct.Name}\" " +
                                     "successfully added to your order."));
            Products -= orderedProduct;
        }

        public void DeleteZeroProduct(Product product)
        {
            Products.Remove(Products.Find(item => item.Name == product.Name));
        }

        public void TakeOrder(Product product)
        {
            if (IsInStore(product))
            {
                var indexOfProduct = Products.IndexOf(Products.Find(item => item.Name == product.Name));
                if (Products[indexOfProduct].QuantityOfProduct >= product.QuantityOfProduct)
                {
                    Products -= product;
                    ProductAction?.Invoke(product,
                        new StoreHandlerArgs($"Product \"{product.Name}\" successfully added to your order."));
                    DeliverTimer(2000);
                }
                else
                {
                    throw new ArgumentException($"Quantity of \"{product.Name}\" is not enough in store. " +
                                                IWarehouse.TAKE_A_WHILE);
                }
            }
            else
            {
                throw new ArgumentException($"Item \"{product.Name}\" was not found in store. " + IWarehouse.TAKE_A_WHILE);
            }

        }

        private bool IsInStore(Product product)
        {
            return Products.Any(item => item.Name == product.Name);
        }

        private void DeliverTimer(int time)
        {
            Thread.Sleep(time);
        }
        private int RandomValuation(int number)
        {
            Random random = new Random();
            return random.Next(number + 1, number*2);
        }
    }
}