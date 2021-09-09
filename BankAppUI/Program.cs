using ApplicationLibrary;
using DataAccessLibrary;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using System;
using System.Linq;

namespace BankAppUI
{
    class Program
    {
        static void Main(string[] args)
        {
            HomeScreen();

            /*var db = new BankContext();
            var accounts = db.Accounts.ToList();
            int i = 1;
            foreach (var acc in accounts)
            {
                var customer = db.Customers.Where(c => c.Id == acc.CustomerId).FirstOrDefault();
                Console.WriteLine($"{i} - {customer}: {acc.AccountType} account with ID {acc.Id}");
                i++;
            }*/
        }

        public static void HomeScreen()
        {
            Console.WriteLine("****************************************Welcome to a simple bank application!****************************************\n");
            Console.WriteLine("1 - Create new customer");
            Console.WriteLine("2 - Open new account");
            Console.WriteLine("3 - Deposit funds");
            Console.WriteLine("4 - Withdraw funds");
            Console.WriteLine("5 - Query account");
            Console.WriteLine("6 - Exit application");
            Console.Write("\nENTER the number that corresponds to the operation you would like to perform: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Clear();
                    CreateNewCustomer();
                    HomeScreen();
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("\nYou would like to open a new account!");
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("\nYou would like to deposit funds!");
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("\nYou would like to withdraw funds!");
                    break;
                case "5":
                    Console.Clear();
                    Console.WriteLine("\nYou would like to query the account!");
                    break;
                case "6":
                    Console.Clear();
                    Console.WriteLine("\nExiting the application. Have a great day!");
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("\nIncorrect input. Exiting the application. Have a great day!");
                    break;
            }
        }

        public static void CreateNewCustomer()
        {
            string name = Utility.GetRawInput("Enter the name of the customer: ");
            Customer cust = new Customer(name);

            string accountType = Utility.GetRawInput("Account type (Enter the number 1 for SAVINGS OR the number 2 for CHEQUING): ");
            if (accountType == "1")
            {
                accountType = "Savings";
            } else if (accountType == "2")
            {
                accountType = "Chequing";
            } else
            {
                Console.WriteLine("Incorrect input. Please enter only a 1 or a 2 to indicate the account type.");
            }
            double startingBalance = Utility.GetNumInput("Enter the starting balance for this account: ");
            Account acc = new Account(startingBalance, accountType, cust.Id);

            using (var db = new BankContext())
            {
                db.Customers.Add(cust);
                db.Accounts.Add(acc);
                db.SaveChanges();
            }

            Console.WriteLine($"Customer {name} has been created and added to the database.");
            Utility.PrintEnterMessage();

            Console.Clear();
        }
    }
}
