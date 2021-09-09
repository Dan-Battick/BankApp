using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLibrary
{
    public static class Utility
    {
        public static string GetRawInput(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        public static double GetNumInput(string message)
        {
            Console.Write(message);
            return double.Parse(Console.ReadLine());
        }

        public static void PrintEnterMessage()
        {
            Console.WriteLine("\nPress enter to continue.");
            Console.ReadKey();
        }
    }
}

