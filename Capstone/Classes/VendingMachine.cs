using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        public Dictionary<string, Item> Inventory { get; private set; } = new Dictionary<string, Item>();
        protected string FilePath { get; private set; }
        public decimal Balance { get; private set; }

        private static decimal startingBalance = 0;

        public VendingMachine(string filePath)
        {
            this.FilePath = filePath;
        }

        public VendingMachine()
        {
        }

        public void StockMachine()
        {
            try
            {
                using (StreamReader sr = new StreamReader(FilePath))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split('|');
                        string productType = line[3];

                        switch (productType)
                        {
                            case "Chip":
                                Chip chip = new Chip(line[1], decimal.Parse(line[2]));
                                Inventory[line[0]] = chip;
                                break;

                            case "Candy":
                                Candy candy = new Candy(line[1], decimal.Parse(line[2]));
                                Inventory[line[0]] = candy;
                                break;

                            case "Drink":
                                Drink drink = new Drink(line[1], decimal.Parse(line[2]));
                                Inventory[line[0]] = drink;
                                break;

                            case "Gum":
                                Gum gum = new Gum(line[1], decimal.Parse(line[2]));
                                Inventory[line[0]] = gum;
                                break;
                        }
                    }
                }
            }
            catch(IOException IOEError)
            {
                Console.WriteLine("Error while reading the Inventory file");
            }
            catch(IndexOutOfRangeException IOOREError) 
            {
                Console.WriteLine("Inventory file incorrect format");
            }
            catch(Exception eError)
            {
                Console.WriteLine("An error occured while processing your request");
            }
        }

        public void DisplayInventory()
        {
            foreach (KeyValuePair<string, Item> kvp in Inventory)
            {
                Console.WriteLine($"{kvp.Key} {kvp.Value.DisplayItem()}");
            }
        }

        public void DisplayCurrentBalance()
        {
            Console.WriteLine();
            Console.WriteLine($"Current Money Provided: {Balance:C2}");
        }

        public void AcceptMoney()
        {
            startingBalance = Balance;
            bool feedingMoney = true;

            while (feedingMoney)
            {
                Console.WriteLine("Please enter a whole dollar amount: ");
                string moneyFed = Console.ReadLine();
                int moneyFedInt = 0;

                try
                {
                    moneyFedInt = int.Parse(moneyFed);

                    if (moneyFedInt < 0)
                    {
                        Console.WriteLine("Cannot accept a negative amount");
                        moneyFedInt = 0;
                        throw new Exception();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid input - please enter a whole dollar amount (e.g. 1, 2, 5)");
                }

                Balance += moneyFedInt;

                Console.WriteLine($"Current money amount: {Balance:C2}. Feed more money? (Y/N)");
                string feedMoreMoney = Console.ReadLine();

                if (feedMoreMoney == "N" || feedMoreMoney == "n")
                {
                    feedingMoney = false;
                }
                else if (!(feedMoreMoney == "Y" || feedMoreMoney == "y"))
                {
                    Console.WriteLine("Invalid input - exiting");
                    feedingMoney = false;
                }
            }

            Logger.Log("FEED MONEY:", startingBalance, Balance);
        }

        public void MakeChange()
        {
            startingBalance = Balance;
            Dictionary<string, int> changeGiven = new Dictionary<string, int>()
            {
                {"quarters", 0 },
                {"dimes", 0 },
                {"nickels", 0 }
            };
            const decimal NICKEL = 0.05M;
            const decimal DIME = 0.10M;
            const decimal QUARTER = 0.25M;

            while (Balance != 0)
            {
                if (Balance >= QUARTER)
                {
                    Balance -= QUARTER;
                    changeGiven["quarters"] += 1;
                }
                else if (Balance >= DIME)
                {
                    Balance -= DIME;
                    changeGiven["dimes"] += 1;
                }
                else if (Balance >= NICKEL)
                {
                    Balance -= NICKEL;
                    changeGiven["nickels"] += 1;
                }
            }

            Logger.Log("GIVE CHANGE:", startingBalance, Balance);
            Console.WriteLine($"Your change is {changeGiven["quarters"]} quarters, {changeGiven["dimes"]} dimes, and {changeGiven["nickels"]} nickels for a total of {startingBalance:C2}");
        }

        public void SelectProduct()
        {
            DisplayInventory();
            Console.WriteLine();
            Console.WriteLine("Please make a selection: ");
            string productSelected = Console.ReadLine().ToUpper();

            if (Inventory.ContainsKey(productSelected))
            {
                if (!Inventory[productSelected].SoldOut)
                {
                    if (Balance >= Inventory[productSelected].Price)
                    {
                        Dispense(productSelected); 
                    }
                    else
                    {
                        Console.WriteLine("Insufficient funds");
                    }
                }
                else
                {
                    Console.WriteLine($"Sorry, {Inventory[productSelected].Name} is sold out");
                }
            }
            else
            {
                Console.WriteLine($"Sorry, {productSelected} is not a valid option");
            }
            ClearScreenHold();
        }

        public void Dispense(string productCode)
        {
            startingBalance = Balance;

            Inventory[productCode].DecreaseStock();
            Balance -= Inventory[productCode].Price;
            Console.WriteLine(Inventory[productCode].ItemMessage());

            Logger.Log($"{Inventory[productCode].Name} {productCode}", startingBalance, Balance);
        }

        public void RunVendingMachine()
        {
            StockMachine();

            bool machineRunning = true;
            while (machineRunning)
            {
                Console.Clear();
                Menu.DisplayMenu(Menu.MainMenuOptions);
                string userSelection = Menu.GetUserInput();
                Console.Clear();

                if (userSelection == "1")
                {
                    DisplayInventory();                    
                    ClearScreenHold();                    
                }
                else if (userSelection == "2")
                {
                    RunPurchaseMenu();
                }
                else if (userSelection == "3")
                {
                    machineRunning = false;
                }
            } 
        }

        public void RunPurchaseMenu()
        {
            bool transactionInProgress = true;

            while(transactionInProgress)
            {
                Console.Clear();
                Menu.DisplayMenu(Menu.PurchaseMenuOptions);
                DisplayCurrentBalance();
                string userSelection = Menu.GetUserInput();
                Console.Clear();

                if (userSelection == "1")
                {
                    AcceptMoney();                    
                }
                else if (userSelection == "2")
                {
                    SelectProduct();
                }
                else if (userSelection == "3")
                {
                    transactionInProgress = false;
                    MakeChange();
                    ClearScreenHold();
                }
            }
        }

        public static void ClearScreenHold()
        {
            Console.WriteLine();
            Console.WriteLine("Please press 'Enter' to go to previous menu");
            Console.ReadLine();
        }
    }
}
