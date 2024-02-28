using System;
using System.IO;
using ConsoleApplication.Problems;
using ProcessActivity;
// static Classes inside project
using static ConsoleApplication.Settings;
using static ConsoleApplication.Loading;
using static ConsoleApplication.Saving;

namespace ConsoleApplication
{
    //TODO refactor by using more classes in Project
    internal class Program
    {
        private static bool _running = true;
        public static string userInput;
        public static string _path;
        public static Activitys _activitys;
        public static string[] UserInputSplit => userInput.Split(' ');

        public static int InputLength => UserInputSplit.Length;

        public static void Main(string[] args)
        {
            LoadUserSettings();
            while (_running)
            {
                RequestUserInput();
            }
        }

        /// <summary>
        /// This is the Main cylce of the program, this will be fired everytime a operation completes.
        /// </summary>
        private static void RequestUserInput()
        {
            userInput = Console.ReadLine();
            if (userInput == null) return;
            try
            {
                switch (UserInputSplit[0].ToLower())
                {
                    case "load":
                        LoadFile();
                        break;
                    case "save":
                        SaveFile();
                        break;
                    case "setting":
                    case "--s":
                        ProcessSettingCommand();
                        break;
                    case "quit":
                    case "q":
                        _running = false;
                        break;
                    default:
                        Warnings.CommandNotFound();
                        break;
                }
            }
            catch (IndexOutOfRangeException)
            {
                Errors.NotEnoughArguments();
            }
            catch (InvalidDataException)
            {
                Errors.InvalidDatatype();
            }
        }
    }
}