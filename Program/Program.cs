using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using DataProcessing.commands;

namespace DataProcessing
{
    internal class Program
    {
        private const string XmlPath = "UserSettings/UserSettings.xml";
        private static string Path;

        // TODO when adding, update the integrity Check
        public static List<string> Name = new List<string>();
        public static List<char> Gender = new List<char>();
        public static List<double> Time = new List<double>();
        public static List<string> Nationality = new List<string>();
        public static List<int> Age = new List<int>();

        public static void Main(string[] args)
        {
            Initialize(XmlPath);
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
                case "filter":
                case "-f":
                    FilterCmd.FilterCommand(userInput);
                    break;
                case "settings":
                case "-s":
                    UserSettings.FilterCommand(userInput);
                    break;
                case "quit":
                case "-q":
                    return false;
                default:
                    Console.WriteLine("Command not found.");
                    break;
            }

            return true;
        }

        private static void Initialize(string path)
        {
            var textReader = new XmlTextReader(path);
            while (textReader.Read())
            {
                if (textReader.NodeType != XmlNodeType.Element || textReader.Name != "path") continue;
                textReader.Read();
                Path = textReader.Value;
                break;
            }
            //TODO read values from a database
        }

        private static bool CheckIntregrity()
        {
            var tSize = Gender.Count; // base the other values on the size of the gender List
            try
            {
                if (Name.Count != tSize) throw new DataException("name");
                if (Gender.Count != tSize) throw new DataException("gender");
                if (Time.Count != tSize) throw new DataException("time");
                if (Nationality.Count != tSize) throw new DataException("nationality");
                if (Age.Count != tSize) throw new DataException("age");
                return true;
            }
            catch (DataException e)     // wrong sizes
            {
                switch (e.Message)
                {
                    case "name":
                        Console.WriteLine("the size of the names is wrong.");
                        goto default;
                    case "gender":
                        Console.WriteLine("the size of the gender is wrong.");
                        goto default;
                    case "time":
                        Console.WriteLine("the size of the time is wrong.");
                        goto default;
                    case "nationality":
                        Console.WriteLine("the size of the nationality is wrong.");
                        goto default;
                    case "age":
                        Console.WriteLine("the size of the age is wrong.");
                        goto default;
                    default:
                        return false;
                }
            }
        }
    }
}