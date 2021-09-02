using DataAccessLibrary;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using System;

namespace BankAppUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer c1 = new Customer("Daniel Battick");
            Account a1 = c1.createAccount(5000, "Savings");
            a1.deposit(4500);
            using (var db = new BankContext())
            {
                db.Customers.Add(c1);
                db.SaveChanges();
            }
        }
    }
}
