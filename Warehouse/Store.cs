using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exceptions;

namespace Warehouse
{
    public sealed class Store : Warehouse, IWarehouse
    {
        public delegate void StoreHandler(object sender, StoreHandlerArgs handlerArgs);

        public Store(List<Product> products)
        {
            _products = products;
        }
        public void ShowExistingProducts()
        {
            if (IsEmpty())
            {
                throw new UnderflowException("Store is empty, deliver new products before taking an order.");
            }
            foreach (var product in _products)
                Console.WriteLine(product);
        }

        public void AddProduct(Product product)
        {
            _products += new Product(product.Name, RandomValuation(product.QuantityOfProduct));
            ProductAction?.Invoke(product,
                new StoreHandlerArgs($"Product \"{product.Name}\"" +
                                     " successfully ordered, wait until it will be delivered. "
                                     + IWarehouse.TAKE_A_WHILE));
            Task.Run(async delegate
            {
                await Task.Delay(10000);
                ProductAction?.Invoke(product,
                    new StoreHandlerArgs($"Product \"{product.Name}\" " +
                                         "successfully ordered from store."));
            });
        }

        public bool IsEmpty()
        {
            return _products.Count == 0;
        }

        public event StoreHandler ProductAction;

        public void DeleteProductFromStore(Product product)
        {
            _products.Remove(_products.Find(item => item.Name == product.Name));
        }

        public bool EnoughQuantity(Product product)
        {
            return _products.Find(item => item.Name == product.Name).QuantityOfProduct >= product.QuantityOfProduct;
        }

        public void TakeOrder(Product product)
        {
            if (IsEmpty()) throw new UnderflowException("Store is empty, deliver new products before taking an order.");
            _products -= product;
            ProductAction?.Invoke(product,
                new StoreHandlerArgs($"Product \"{product.Name}\" successfully taken from store."));
        }

        private int RandomValuation(int number)
        {
            var random = new Random();
            return random.Next(number + 1, number * 2);
        }
    }
}