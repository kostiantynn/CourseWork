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

        public event ProductHandler ProductAction;

        public override void ShowExistingProducts()
        {
            if (IsEmpty()) throw new UnderflowException(Constants.EmptyOrder);
            base.ShowExistingProducts();
        }

        public void AddProductsToTheOrder(Store store, Product newProduct)
        {
            if (!store.IsInWarehouse(newProduct) ||
                store.IsInWarehouse(newProduct) && !store.EnoughQuantity(newProduct))
            {
                if (store.Status == Status.Delivery)
                {
                    ProductAction?.Invoke(this,
                        new StoreHandlerArgs($"\"{newProduct.Name}\" is already being delivered."));
                }
                else
                {
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