using System;
using static DataProcessing.commands.FilterCmd;
using System.Collections.Generic;

namespace DataProcessing
{
    internal class Program
    {
        public static List<char> Gender = new List<char>{ 'm', 'v', 'm', 'm', 'v', 'v', 'v', 'm', 'm' };

        public static void Main(string[] args)
        {
            while (RequestUserInput())
            {
            }
        }

        private static bool RequestUserInput()
        {
            var userInput = Console.ReadLine()?.Split(' ');
            if (userInput == null) return true;
            switch (userInput[0])
            {
                // filter
                case "filter":
                case "-f":
                    FilterCommand(userInput);
                    break;
                case "q":
                case "quit":
                    return false;
                default:
                    Console.WriteLine("Command not found.");
                    break;
            }

            return true;
        }
    }
}