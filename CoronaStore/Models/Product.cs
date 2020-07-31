﻿

namespace CoronaStore.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int SupplierID { get; set; }

        public int CategoryID { get; set; }

        public string Name { get; set; }

        public string Image
        {
            get;
            set;
        }
    }
}