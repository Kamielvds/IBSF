using System;

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
            var userInput = Console.ReadLine();
            switch (userInput)
            {
                case "-f":
                    
                case "q":
                case "quit":
                    return false;
            }
            return true;
        }
    }
}