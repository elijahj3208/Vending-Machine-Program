using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Gum : Item
    {
        private const string MESSAGE = "Chew Chew, Yum!";

        public Gum(string name, decimal price) : base(name, price)
        {
        }

        public override string ItemMessage()
        {
            return MESSAGE;
        }
    }
}
