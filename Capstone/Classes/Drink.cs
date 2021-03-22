using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Drink : Item
    {
        private const string MESSAGE = "Glug Glug, Yum!";

        public Drink(string name, decimal price) : base(name, price)
        {
        }

        public override string ItemMessage()
        {
            return MESSAGE;
        }
    }
}

