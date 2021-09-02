using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Account> Accounts { get; set; } = new List<Account>();
        
        public Customer(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }

        public Account createAccount(double balance, string type)
        {
            Account acc = new Account(balance, type);
            acc.CustomerId = Id;
            Accounts.Add(acc);
            return acc;
        }
        /// <summary>
        /// Retrieve the accounts that the customer has.
        /// </summary>
        /// <returns>A string that lists all the accounts that the customer has.</returns>
        public string getAccounts()
        {
            string str = "";
            int numAccounts = 1;
            if (Accounts.Count == 0)
            {
                return "No accounts have been created for this customer.";
            }
            else
            {
                foreach (Account acc in Accounts)
                {
                    str += $"\n{numAccounts}. {acc.ToString()}";
                    numAccounts++;
                }
            }
            return str;
        }

        public override string ToString()
        {
            return $"Customer Name: {Name}\nAccount(s):{getAccounts()}";
        }
    }
}
