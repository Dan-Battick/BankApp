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
            var op1 = HomeScreen();

            switch (op1)
            {
                case "1":
                    Console.WriteLine("\nYou would like to create a new customer!");
                    break;
                case "2":
                    Console.WriteLine("\nYou would like to view created customers!");
                    break;
                case "3":
                    Console.WriteLine("\nExiting the application. Have a great day!");
                    break;
                default:
                    Console.WriteLine("\nIncorrect input. Exiting the application. Have a great day!");
                    break;
            }
        }

        public static string HomeScreen()
        {
            Console.WriteLine("****************************************Welcome to a simple bank application!****************************************\n");
            Console.WriteLine("1 - Create new customer");
            Console.WriteLine("2 - View customers");
            Console.WriteLine("3 - Exit application");
            Console.Write("\nSELECT the number that corresponds to the operation you would like to perform: ");

            string choice = Console.ReadLine();
            return choice;
        }
    }
}
