using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public static class Menu
    {
        public static List<string> MainMenuOptions { get; set; } = new List<string>() { "(1) Display Vending Machine Items", "(2) Purchase", "(3) Exit" };

        public static List<string> PurchaseMenuOptions { get; set; } = new List<string>() { "(1) Feed Money", "(2) Select Product", "(3) Finish Transaction" };

        public static void DisplayMenu(List<string> menuOptions)
        {
            foreach (string option in menuOptions)
            {
                Console.WriteLine(option);
            }
        }

        public static string GetUserInput()
        {
            bool isNotCorrectFormat = true; 
            string inputString = Console.ReadLine();
            
            while (isNotCorrectFormat)
            {
                if (inputString != "1" ^ inputString != "2" ^ inputString != "3")
                {
                    Console.WriteLine("Please select a valid option");
                    inputString = Console.ReadLine();
                }
                else
                {
                    isNotCorrectFormat = false;
                }
            }

            return inputString;
        }
    }
}
