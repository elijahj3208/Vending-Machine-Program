using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Candy : Item
    {
        private const string MESSAGE = "Munch Munch, Yum!";

        public Candy(string name, decimal price) : base(name, price)
        {
        }

        public override string ItemMessage()
        {
            return MESSAGE;
        }
    }
}
