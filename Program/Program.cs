using System;
using static DataProcessing.commands.FilterCmd;
using System.Collections.Generic;

namespace DataProcessing
{
    internal class Program
    {
        public static List<string> Name = new List<string>();
        public static List<char> Gender = new List<char> ();
        public static List<double> Time = new List<double>();
        public static List<string> Nationality = new List<string>();

        public static void Main(string[] args)
        {
            if (CheckIntregrity())
                while (RequestUserInput())
                {
                }

            Console.WriteLine("The given data is invalid");
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

        private static void Initialize(string givenPath)
        {
        }

        private static bool CheckIntregrity()
        {
            var tSize = Gender.Count;
            if (Name.Count != tSize) return false;
            if (Time.Count != tSize) return false;
            return true;
        }
    }
}