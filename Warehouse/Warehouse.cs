using System.Collections.Generic;
using System.Linq;

namespace Warehouse
{
    public abstract class Warehouse
    {
        protected List<Product> _products;
        
        internal bool IsInWarehouse(Product product)
        {
            return _products.Any(item => item.Name == product.Name);
        }
    }
}