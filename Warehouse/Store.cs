using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Exceptions;

namespace Warehouse
{
    public sealed class Store : Warehouse, IWarehouse
    {
        // Queue for products during delivery
        internal readonly DeliveryQueue DeliveryQueue = new DeliveryQueue();

        public Store(List<Product> products)
        {
            _products = products;
        }

        // Public interface methods implementation
        public void AddProduct(Product product)
        {
            ProductAction?.Invoke(product,
                new StoreHandlerArgs($"Product \"{product.Name}\"" +
                                     $" successfully ordered, wait until it will be delivered. {Constants.TakeAWhile}"));
            Task.Run(async delegate
            {
                DeliveryQueue.AddProduct(product);
                await Task.Delay(Constants.DeliveryTime);
                _products += new Product(product.Name, RandomValuation(product.QuantityOfProduct));
                ProductAction?.Invoke(product,
                    new StoreHandlerArgs($"Product \"{product.Name}\" " +
                                         "is successfully delivered to the store."));
                DeliveryQueue.DeleteProduct(product.Name);
            });
        }

        public void DeleteProduct(string productName)
        {
            _products.Remove(_products.Find(item => item.Name == productName));
        }

        // Event for displaying actions with product
        public event ProductHandler ProductAction;

        // Virtual function overriding from parent class "Warehouse"
        public override void ShowExistingProducts()
        {
            if (IsEmpty()) throw new UnderflowException(Constants.EmptyStore);
            ProductAction?.Invoke(this, new StoreHandlerArgs(Constants.StoreListing));
            base.ShowExistingProducts();
        }

        // Public methods for working with Products at store
        public void TakeOrder(Product product)
        {
            if (IsEmpty()) throw new UnderflowException(Constants.EmptyStore);
            if (DeliveryQueue.IsInWarehouse(product.Name))
                throw new ThreadStateException(Constants.DeliveredYet);
            _products -= product;
            ProductAction?.Invoke(product,
                new StoreHandlerArgs($"Product \"{product.Name}\" successfully taken from store."));
        }

        public void ShowDeliveryQueue()
        {
            ProductAction?.Invoke(this, new StoreHandlerArgs(Constants.DeliveryQueueListing));
            DeliveryQueue.ShowExistingProducts();
        }
        // Internal and private methods
        internal bool EnoughQuantity(Product product)
        {
            return _products.Find(item => item.Name == product.Name).QuantityOfProduct >= product.QuantityOfProduct;
        }

        private int RandomValuation(int number)
        {
            var random = new Random();
            return random.Next(number + 1, number * 2);
        }
    }
}