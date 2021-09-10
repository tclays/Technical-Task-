using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineTask
{
    class Product : ICustomer
    {

        private string mName;
        private double mPrice;
        public Product()
        {

        }

        public Product(string name, double price)
        {
            mName = name;
            mPrice = price;
        }

        public string Name { get => mName; set => mName = value; }
        public double Price { get => mPrice; set => mPrice = value; }

    }
}
