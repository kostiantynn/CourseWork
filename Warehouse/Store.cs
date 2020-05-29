using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Exceptions;

namespace Warehouse
{
    public sealed class Store : Warehouse, IWarehouse
    {
        public Status Status = Status.Free;

        public Store(List<Product> products)
        {
            _products = products;
        }

        public void AddProduct(Product product)
        {
            ProductAction?.Invoke(product,
                new StoreHandlerArgs($"Product \"{product.Name}\"" +
                                     $" successfully ordered, wait until it will be delivered. {Constants.TakeAWhile}"));
            Task.Run(async delegate
            {
                Status = Status.Delivery;
                await Task.Delay(Constants.DeliveryTime);
                _products += new Product(product.Name, RandomValuation(product.QuantityOfProduct));
                ProductAction?.Invoke(product,
                    new StoreHandlerArgs($"Product \"{product.Name}\" " +
                                         "is in your order."));
                Status = Status.Free;
            });
        }

        public bool IsEmpty()
        {
            return _products.Count == 0;
        }

        public event ProductHandler ProductAction;

        public override void ShowExistingProducts()
        {
            if (IsEmpty()) throw new UnderflowException(Constants.EmptyStore);
            base.ShowExistingProducts();
        }

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
            if (IsEmpty()) throw new UnderflowException(Constants.EmptyStore);
            if (Status == Status.Delivery) throw new ThreadStateException("Product hasn't been delivered yet.");
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