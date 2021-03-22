using System;
using Capstone.Classes;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            string inventoryFile = @"C:\Users\Student\workspace\module1-capstone-c-team-3\Capstone\dotnet\vendingmachine.csv";

            VendingMachine vendingMachine = new VendingMachine(inventoryFile);
            vendingMachine.RunVendingMachine();

        }
    }
}
