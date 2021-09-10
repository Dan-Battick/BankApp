using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLibrary
{
    public static class Utility
    {   
        /// <summary>
        /// Utility tool to read in the user's input choice.
        /// </summary>
        /// <param name="message">
        /// This is the prompt message for the user to respond to.
        /// </param>
        /// <returns>Returns the user's response as a string</returns>
        public static string GetRawInput(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        /// <summary>
        /// Utility tool to read in the user's input choice as a double.
        /// </summary>
        /// <param name="message">
        /// This is the prompt message for the user to respond to.
        /// </param>
        /// <returns>Returns the user's response as a double</returns>
        public static double GetNumInput(string message)
        {
            Console.Write(message);
            return double.Parse(Console.ReadLine());
        }

        /// <summary>
        /// Utility tool that prompts user to press enter in order to continue.
        /// </summary>
        public static void PrintEnterMessage()
        {
            Console.WriteLine("\nPress enter to continue.");
            Console.ReadKey();
        }
    }
}

