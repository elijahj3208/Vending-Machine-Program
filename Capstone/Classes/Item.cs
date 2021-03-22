using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public abstract class Item
    {
        //properties
        private const int START_COUNT = 5;

        public string Name { get; private set; }

        public decimal Price { get; private set; }

        public int StockCount { get; private set; }

        public bool SoldOut
        {
            get
            {
                bool output = false;

                if (StockCount <= 0)
                {
                    output = true;
                }

                return output;
            }
        }

        //constructor
        public Item(string name, decimal price)
        {
            this.Name = name;
            this.Price = price;
            this.StockCount = START_COUNT;
        }

        //methods
        public void DecreaseStock()
        {
            StockCount--;
        }

        public string DisplayItem()
        {
            string quantity = "";

            if (SoldOut)
            {
                quantity = "SOLD OUT";
            }
            else
            {
                quantity += StockCount;
            }

            string output = $"{Name} {Price} {quantity}";

            return output;
        }

        public abstract string ItemMessage();

    }
}
