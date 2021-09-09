using ApplicationLibrary;
using DataAccessLibrary;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BankAppUI
{
    class Program
    {

        static void Main(string[] args)
        {
            HomeScreen();
            //DepositFunds();
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
                    OpenNewAccount();
                    HomeScreen();
                    break;
                case "3":
                    Console.Clear();
                    DepositFunds();
                    HomeScreen();
                    break;
                case "4":
                    Console.Clear();
                    WithDrawFunds();
                    HomeScreen();
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
            Console.WriteLine("*********CREATE NEW CUSTOMER*********\n");
            string name = Utility.GetRawInput("Enter the name of the customer: ");
            Customer cust = new Customer(name);

            Account acc = CreateNewAccount(cust.Id);

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

        private static Account CreateNewAccount(Guid customerId)
        {
            string accountType = Utility.GetRawInput("Account type (Enter the number 1 for SAVINGS OR the number 2 for CHEQUING): ");
            if (accountType == "1")
            {
                accountType = "Savings";
            }
            else if (accountType == "2")
            {
                accountType = "Chequing";
            }
            else
            {
                Console.WriteLine("Incorrect input. Please enter only a 1 or a 2 to indicate the account type.");
            }
            double startingBalance = Utility.GetNumInput("Enter the starting balance for this account: ");
            Account acc = new Account(startingBalance, accountType, customerId);
            return acc;
        }

        public static void OpenNewAccount()
        {
            Console.WriteLine("*********OPEN NEW ACCOUNT*********\n");
            Console.WriteLine("Would you like to open an account for an existing customer or for a new customer?");
            string choice = Utility.GetRawInput("1 - Existing Customer    OR     2 - New Customer\nEnter your choice(1 or 2): ");
            switch (choice)
            {
                case "1":
                    Console.Clear();
                    OpenAccForExistingCust();
                    break;
                case "2":
                    Console.Clear();
                    CreateNewCustomer();
                    break;
            }
        }

        public static List<Customer> ViewCustomers()
        {
            var db = new BankContext();
            var accounts = db.Accounts.ToList();
            List<Customer> customers = new List<Customer>();
            foreach (var acc in accounts)
            {
                //Console.WriteLine(acc);
                var customer = db.Customers.Where(c => c.Id == acc.CustomerId).FirstOrDefault();
                //Console.WriteLine(customer);
                //customer.Accounts.Add(acc);
                if (customers.Contains(customer))
                {
                    continue;
                }
                else
                {
                    customers.Add(customer);
                }
                //Console.WriteLine($"{customer.Name}: {acc.AccountType} account with ID {acc.Id}\n");
            }
            int i = 1;
            Console.WriteLine("Below is the list of customers:");
            foreach (var cust in customers)
            {
                Console.WriteLine($"Customer # {i} - {cust}\n\n");
                i++;
            }
            return customers;
        }

        private static Customer GetCustomer(string message)
        {
            var customers = ViewCustomers();
            string customerNum = Utility.GetRawInput(message);
            int actualCustNum = int.Parse(customerNum) - 1;
            Customer cust = customers[actualCustNum];
            return cust;
        } 

        public static void OpenAccForExistingCust()
        {
            Customer existingCust = GetCustomer("Which customer would you like to open the new account for? Enter the customer #: ");
            Console.Clear();
            Console.WriteLine($"You are opening a new account for {existingCust.Name}, ID {existingCust.Id}");
            Account acc = CreateNewAccount(existingCust.Id);
            using (var db = new BankContext())
            {
                db.Accounts.Add(acc);
                db.SaveChanges();
            }

            Console.WriteLine($"New account has been opened for {existingCust.Name}.");
            Utility.PrintEnterMessage();

            Console.Clear();
        }

        private static Account GetAccount(Customer cust, string message)
        {
            string accountNum = Utility.GetRawInput(message);
            int actualAccNum = int.Parse(accountNum) - 1;
            Account acc = cust.Accounts[actualAccNum];
            return acc;
        }

        public static void DepositFunds()
        {
            Console.WriteLine("********DEPOSIT FUNDS********");
            Customer cust = GetCustomer("\nWhich customer owns the account to deposit funds into? Enter the customer #: ");
            Console.Clear();

            Console.WriteLine($"{cust.Name} with ID {cust.Id} has the following accounts:\n");
            int i = 1;
            foreach (var acct in cust.Accounts)
            {
                Console.WriteLine($"Account # {i} - {acct.AccountType} account; Balance {acct.Balance.ToString("C")}; ID {acct.Id}");
                i++;
            }
            Account acc = GetAccount(cust, "\nWhich account should the funds be deposited into? Enter the account #: ");
            double amount = Utility.GetNumInput("\nEnter the amount to be deposited: ");
            acc.Balance += amount;
            Transaction trans = new Transaction(amount, "Deposit", acc.Id);
            Console.WriteLine($"The account balance had been updated. The current balance is {acc.Balance}");
            using (var db = new BankContext())
            {
                db.Accounts.Update(acc);
                db.Transactions.Add(trans);
                db.SaveChanges();
            }

            Utility.PrintEnterMessage();
            Console.Clear();
        }

        public static void WithDrawFunds()
        {
            Console.WriteLine("********WITHDRAW FUNDS********");
            Customer cust = GetCustomer("\nWhich customer owns the account to withdraw funds from? Enter the customer #: ");
            Console.Clear();

            Console.WriteLine($"{cust.Name} with ID {cust.Id} has the following accounts:\n");
            int i = 1;
            foreach (var acct in cust.Accounts)
            {
                Console.WriteLine($"Account # {i} - {acct.AccountType} account; Balance {acct.Balance.ToString("C")}; ID {acct.Id}");
                i++;
            }
            Account acc = GetAccount(cust, "\nWhich account should the funds be withdrawn from? Enter the account #: ");
            double amount = Utility.GetNumInput("\nEnter the amount to be withdrawn: ");
            acc.Balance -= amount;
            Transaction trans = new Transaction(amount, "Withdraw", acc.Id);
            Console.WriteLine($"The account balance had been updated. The current balance is {acc.Balance}");
            using (var db = new BankContext())
            {
                db.Accounts.Update(acc);
                db.Transactions.Add(trans);
                db.SaveChanges();
            }

            Utility.PrintEnterMessage();
            Console.Clear();
        }
    }
}
