using System;
using System.Collections;
using DataProcessing.commands;
using static DataProcessing.commands.FilterCmd;

namespace DataProcessing
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            while (RequestUserInput()){}
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