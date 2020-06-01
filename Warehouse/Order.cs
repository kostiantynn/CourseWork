using System;
using System.Collections.Generic;
using Exceptions;

namespace Warehouse
{
    public class Order : Warehouse, IWarehouse
    {
        public Order()
        {
            _products = new List<Product>();
        }
        // Public interface methods implementation
        public void AddProduct(Product product)
        {
            _products += product;
        }

        public void DeleteProduct(string productName)
        {
            if (IsInWarehouse(productName))
            {
                ProductAction?.Invoke(this,
                    new StoreHandlerArgs($"Product \"{productName}\" was removed from your order."));
                _products.Remove(_products.Find(item => item.Name == productName));
            }
            else
            {
                throw new ArgumentException($"Product \"{productName}\" was not found in your order.");
            }
        }

        // Public method for working with order
        public int GetSizeOfOrder()
        {
            return _products.Count;
        }

        public event ProductHandler ProductAction;

        public override void ShowExistingProducts()
        {
            ProductAction?.Invoke(this, new StoreHandlerArgs(Constants.OrderListing));
            if (IsEmpty()) throw new UnderflowException(Constants.EmptyOrder);
            base.ShowExistingProducts();
        }
        public void AddProductsToTheOrder(Store store, Product newProduct)
        {
            if (!store.IsInWarehouse(newProduct.Name) ||
                store.IsInWarehouse(newProduct.Name) && !store.EnoughQuantity(newProduct))
            {
                if (store.DeliveryQueue.IsInWarehouse(newProduct.Name))
                {
                    ProductAction?.Invoke(this,
                        new StoreHandlerArgs($"\"{newProduct.Name}\" is delivering."));
                }
                else
                {
                    ProductAction?.Invoke(this,
                        new StoreHandlerArgs(
                            $"Product \"{newProduct.Name}\" is not enough in store or it doesn't exist." +
                            Constants.DeliverSoon));
                    store.AddProduct(newProduct);
                    AddProduct(newProduct);
                }
            }
            else
            {
                AddProduct(newProduct);
            }
        }
    }
}