using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Chip : Item
    {
        private const string MESSAGE = "Crunch Crunch, Yum!";

        public Chip(string name, decimal price) : base(name, price)
        {
        }

        public override string ItemMessage()
        {
            return MESSAGE;
        }
    }
}
