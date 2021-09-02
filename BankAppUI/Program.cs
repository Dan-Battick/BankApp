using DataAccessLibrary;
using DataAccessLibrary.Models;
using System;

namespace BankAppUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer cus1 = new Customer("Daniel Battick");
            //Console.WriteLine(cus1);
            Account acc1 = cus1.createAccount(8000, "Savings");
            //Account acc2 = cus1.createAccount(20000, "Chequing");
            //string accs = cus1.getAccounts();
            //Console.WriteLine(cus1);
            //Console.WriteLine("*********************************************");
            acc1.deposit(4500);
            bool withdrawn = acc1.withdraw(7000);
            acc1.deposit(4500);
            acc1.deposit(1200);
            acc1.deposit(5600);
            bool withdrawn2 = acc1.withdraw(5000);
            acc1.deposit(400);
            bool withdrawn3 = acc1.withdraw(6800);
            acc1.deposit(500);
            acc1.deposit(100);
            acc1.deposit(560000);
            bool withdrawn4 = acc1.withdraw(75000);
            acc1.queryAccount();
            //acc2.queryAccount();
        }
    }
}
