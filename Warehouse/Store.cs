using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Warehouse
{
    public sealed class Store : Warehouse, IWarehouse
    {
        
        public delegate void StoreHandler(object sender, StoreHandlerArgs handlerArgs);
        public event StoreHandler ProductAction;
        
        public Store(List<Product> products)
        {
            _products = products;
        }

        public void AddProduct(Product product)
        {
            _products += new Product(product.Name, RandomValuation(product.QuantityOfProduct));
            ProductAction?.Invoke(product, 
                new StoreHandlerArgs($"Product \"{product.Name}\"" +
                                     " successfully ordered, wait until it will be delivered. " 
                                     + IWarehouse.TAKE_A_WHILE));
            DeliverTimer(3000);
            ProductAction?.Invoke(product, 
                new StoreHandlerArgs($"Product \"{product.Name}\" " +
                                     "successfully taken from store."));
            _products -= product;
        }

        public void DeleteZeroProduct(Product product)
        {
            _products.Remove(_products.Find(item => item.Name == product.Name));
        }

        public void TakeOrder(Product product)
        {
            if (IsInStore(product))
            {
                var indexOfProduct = _products.IndexOf(_products.Find(item => item.Name == product.Name));
                if (_products[indexOfProduct].QuantityOfProduct >= product.QuantityOfProduct)
                {
                    _products -= product;
                    ProductAction?.Invoke(product,
                        new StoreHandlerArgs($"Product \"{product.Name}\" successfully taken from store."));
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
            return _products.Any(item => item.Name == product.Name);
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

        public bool IsEmpty()
        {
            return _products.Count == 0;
        }
    }
}