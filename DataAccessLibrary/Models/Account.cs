using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public double Balance { get; set; }
        public string AccountType { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public Guid CustomerId { get; set; }

        public Account(double bal, string type)
        {
            Balance = bal;
            AccountType = type;
            Id = Guid.NewGuid();
        }

        public void deposit(double amt)
        {
            Balance += amt;
            Transaction trans = new Transaction(amt, "Deposit");
            trans.AccountId = Id;
            Transactions.Add(trans);
        }

        public bool withdraw(double amt)
        {
            if ((Balance - amt) < 0)
            {
                Console.WriteLine($"The amount for withdrawal exceeds your current balance of {Balance.ToString("C")}.");
                return false;
            }
            else
            {
                Balance -= amt;
                Transaction trans = new Transaction(amt, "Withdraw");
                trans.AccountId = Id;
                Transactions.Add(trans);
                return true;
            }
        }

        public void queryAccount()
        {
            Console.WriteLine($"The current balance of the account is: {Balance}");
            Console.WriteLine("Below are the account's last 10 transactions:");
            if (Transactions.Count == 0)
            {
                Console.WriteLine("***No transactions have been performed on this account as yet.***");
            }
            else if (Transactions.Count >= 1 && Transactions.Count < 10)
            {
                Transactions.Reverse();
                foreach(Transaction trans in Transactions)
                {
                    Console.WriteLine(trans);
                }
            }
            else
            {
                Transactions.Reverse();
                for (int x=0; x <= 9; x++)
                {
                    Console.WriteLine(Transactions[x]);
                }
            }
        }

        public override string ToString()
        {
            return $"{AccountType} account: Current balance is {Balance.ToString("C")}";

        }

    }
}
