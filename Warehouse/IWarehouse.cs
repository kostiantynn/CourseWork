﻿namespace Warehouse
{
    public interface IWarehouse
    {
        static readonly string TAKE_A_WHILE = "It will take a while to deliver new products.";

        public void AddProduct(Product product);
        public bool IsEmpty();
    }
}