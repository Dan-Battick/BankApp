using ApplicationLibrary;
using DataAccessLibrary;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankAppUI
{
    class Program
    {

        static void Main(string[] args)
        {
            ApplicationScreen();
        }

        public static void ApplicationScreen()
        {
            Console.WriteLine("****************************************Welcome to a simple bank application!****************************************\n");
            Console.WriteLine("1 - Create new customer");
            Console.WriteLine("2 - Open new account for existing customer");
            Console.WriteLine("3 - Deposit funds");
            Console.WriteLine("4 - Withdraw funds");
            Console.WriteLine("5 - Query account transactions");
            Console.WriteLine("6 - Exit application");
            Console.Write("\nENTER the number that corresponds to the operation you would like to perform: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Clear();
                    CreateNewCustomer();
             
                    ApplicationScreen();
                    break;
                case "2":
                    Console.Clear();
                    OpenNewAccount();
                    ApplicationScreen();
                    break;
                case "3":
                    Console.Clear();
                    DepositFunds();
                    ApplicationScreen();
                    break;
                case "4":
                    Console.Clear();
                    WithDrawFunds();
                    ApplicationScreen();
                    break;
                case "5":
                    Console.Clear();
                    QueryAccount();
                    ApplicationScreen();
                    break;
                case "6":
                    Console.Clear();
                    Console.WriteLine("\nExiting the application. Have a great day!");
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine("\nIncorrect input. You will be returned to the main screen.");
                    Utility.PrintEnterMessage();
                    Console.Clear();
                    ApplicationScreen();
                    break;
            }
        }

        public static void CreateNewCustomer()
        {
            Console.WriteLine("*********CREATE NEW CUSTOMER*********\n");
            string name = Utility.GetRawInput("Enter the name of the customer or -1 to CANCEL operation: ");
            if (name == "-1")
            {
                Console.WriteLine("Operation cancelled.");
                Utility.PrintEnterMessage();
                Console.Clear();
            } else
            {
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
        }

        public static void OpenNewAccount()
        {
            Console.WriteLine("*********OPEN NEW ACCOUNT*********\n");
            Customer cust = GetCustomer("Which customer would you like to open the new account for? Enter the customer #  or -1 to CANCEL THE OPERATION: ");
            if (cust.Name == "")
            {
                Console.WriteLine("Operation cancelled.");
                Utility.PrintEnterMessage();
                Console.Clear();
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"You are opening a new account for {cust.Name}, ID {cust.Id}");
                Account acc = CreateNewAccount(cust.Id);
                using (var db = new BankContext())
                {
                    db.Accounts.Add(acc);
                    db.SaveChanges();
                }

                Console.WriteLine($"New account has been opened for {cust.Name}.");
                Utility.PrintEnterMessage();

                Console.Clear();
            }
        }

        public static void DepositFunds()
        {
            Console.WriteLine("********DEPOSIT FUNDS********");
            Customer cust = GetCustomer("\nWhich customer owns the account to deposit funds into? Enter the customer # or -1 to CANCEL THE OPERATION: ");
            if (cust.Name == "")
            {
                Console.WriteLine("Operation cancelled.");
                Utility.PrintEnterMessage();
                Console.Clear();
            }
            else
            {
                Console.Clear();

                Console.WriteLine($"{cust.Name} with ID {cust.Id} has the following accounts:\n");
                int i = 1;
                foreach (var acct in cust.Accounts)
                {
                    Console.WriteLine($"Account # {i} - {acct.AccountType} account; Balance {acct.Balance.ToString("C")}; ID {acct.Id}");
                    i++;
                }
                Account acc = GetAccount(cust, "\nWhich account should the funds be deposited into? Enter the account # or -1 to CANCEL THE OPERATION: ");
                if (acc.AccountType == "Default")
                {
                    Console.WriteLine("Operation cancelled.");
                    Utility.PrintEnterMessage();
                    Console.Clear();
                }
                else
                {
                    double amount = Utility.GetNumInput("\nEnter the amount to be deposited: ");
                    acc.Balance += amount;
                    Transaction trans = new Transaction(amount, "Deposit", acc.Id);
                    Console.WriteLine($"The account balance had been updated. The current balance is {acc.Balance.ToString("C")}");
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

        public static void WithDrawFunds()
        {
            Console.WriteLine("********WITHDRAW FUNDS********");
            Customer cust = GetCustomer("\nWhich customer owns the account to withdraw funds from? Enter the customer # or -1 to CANCEL THE OPERATION: ");
            if (cust.Name == "")
            {
                Console.WriteLine("Operation cancelled.");
                Utility.PrintEnterMessage();
                Console.Clear();
            }
            else
            {
                Console.Clear();

                Console.WriteLine($"{cust.Name} with ID {cust.Id} has the following accounts:\n");
                int i = 1;
                foreach (var acct in cust.Accounts)
                {
                    Console.WriteLine($"Account # {i} - {acct.AccountType} account; Balance {acct.Balance.ToString("C")}; ID {acct.Id}");
                    i++;
                }
                Account acc = GetAccount(cust, "\nWhich account should the funds be withdrawn from? Enter the account # or -1 to CANCEL THE OPERATION: ");
                if (acc.AccountType == "Default")
                {
                    Console.WriteLine("Operation cancelled.");
                    Utility.PrintEnterMessage();
                    Console.Clear();
                }
                else
                {
                    double amount = Utility.GetNumInput("\nEnter the amount to be withdrawn: ");
                    acc.Balance -= amount;
                    Transaction trans = new Transaction(amount, "Withdraw", acc.Id);
                    Console.WriteLine($"The account balance had been updated. The current balance is {acc.Balance.ToString("C")}");
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

        public static void QueryAccount()
        {
            Console.WriteLine("********QUERY ACCOUNT TRANSACTIONS********");
            Customer cust = GetCustomer("\nWhich customer owns the account that you would like to query? Enter the customer # or -1 to CANCEL THE OPERATION: ");
            if (cust.Name == "")
            {
                Console.WriteLine("Operation cancelled.");
                Utility.PrintEnterMessage();
                Console.Clear();
            }
            else
            {
                Console.Clear();

                Console.WriteLine($"{cust.Name} with ID {cust.Id} has the following accounts:\n");
                int i = 1;
                foreach (var acct in cust.Accounts)
                {
                    Console.WriteLine($"Account # {i} - {acct.AccountType} account; Balance {acct.Balance.ToString("C")}; ID {acct.Id}");
                    i++;
                }
                Account acc = GetAccount(cust, "\nWhich account would you like to query for past transactions? Enter the account # or -1 to CANCEL THE OPERATION: ");
                if (acc.AccountType == "Default")
                {
                    Console.WriteLine("Operation cancelled.");
                    Utility.PrintEnterMessage();
                    Console.Clear();
                } else
                {
                    ShowTransactions(acc);

                    Utility.PrintEnterMessage();
                    Console.Clear();
                }
            }
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

        private static List<Customer> ViewCustomers()
        {
            var db = new BankContext();
            var accounts = db.Accounts.ToList();
            List<Customer> customers = new List<Customer>();
            foreach (var acc in accounts)
            {
                var customer = db.Customers.Where(c => c.Id == acc.CustomerId).FirstOrDefault();
                if (customers.Contains(customer))
                {
                    continue;
                }
                else
                {
                    customers.Add(customer);
                }
            }
            int i = 1;
            Console.WriteLine("Below is the list of customers:\n");
            foreach (var cust in customers)
            {
                Console.WriteLine($"Customer #{i} - {cust.Name}; {cust.Id}\n\n");
                i++;
            }
            return customers;
        }

        private static Customer GetCustomer(string message)
        {
            var customers = ViewCustomers();
            bool input = false;
            do
            {
                try
                {
                    string customerNum = Utility.GetRawInput(message);

                    if (customerNum == "-1")
                    {
                        return new Customer("");
                    }
                    else
                    {
                        int actualCustNum = int.Parse(customerNum) - 1;
                        Customer cust = customers[actualCustNum];
                        return cust;
                    }
                }
                catch (Exception e)
                {
                    if ((e is FormatException) || (e is ArgumentOutOfRangeException))
                    {
                        Console.WriteLine("Incorrect input. Enter the number that corresponds to the customer of your choice.");
                        input = false;
                    }
                }
            } while (!input);
            return new Customer("");
        }

        private static Account GetAccount(Customer cust, string message)
        {
            bool input = false;
            do
            {
                try
                {
                    string accountNum = Utility.GetRawInput(message);
                    if (accountNum == "-1")
                    {
                        return new Account();
                    }
                    else
                    {
                        int actualAccNum = int.Parse(accountNum) - 1;
                        Account acc = cust.Accounts[actualAccNum];
                        return acc;
                    }
                }
                catch (Exception e)
                {
                    if ((e is FormatException) || (e is ArgumentOutOfRangeException))
                    {
                        Console.WriteLine("Incorrect input. Enter the number that corresponds to the account of your choice.");
                        input = false;
                    }
                }
            } while (!input);
            return new Account();
        }

        private static void ShowTransactions(Account acc)
        {
            var db = new BankContext();
            var transactions = db.Transactions.ToList();

            foreach (var trans in transactions)
            {
                if (acc.Id == trans.AccountId)
                {
                    acc.Transactions.Add(trans);
                }
                else
                {
                    continue;
                }
            }

            Console.WriteLine($"The current balance of the account is: {acc.Balance.ToString("C")}");
            Console.WriteLine("Below are the account's last 10 transactions:\n");
            if (acc.Transactions.Count == 0)
            {
                Console.WriteLine("***No transactions have been performed on this account as yet.***");
            }
            else if (acc.Transactions.Count >= 1 && acc.Transactions.Count < 10)
            {
                //acc.Transactions.Reverse();
                foreach (Transaction trans in acc.Transactions)
                {
                    Console.WriteLine(trans);
                }
            }
            else
            {
                //acc.Transactions.Reverse();
                for (int x = 0; x <= 9; x++)
                {
                    Console.WriteLine(acc.Transactions[x]);
                }
            }
        }
    }
}
