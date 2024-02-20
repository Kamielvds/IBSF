using System;
using System.IO;
using ConsoleApplication.Problems;
using IBSF.Problems;
using ProcessActivity;

namespace ConsoleApplication
{
    internal class Program
    {
        private static bool _running = true;
        private static string _xmlPath;
        private static Activitys _activitys;

        public static void Main(string[] args)
        {
            //TODO Make xml settings file and read it here
            while (_running)
            {
                RequestUserInput();
            }
        }

        private static void RequestUserInput()
        {
            string userInput = Console.ReadLine();
            if (userInput == null) return;
            string[] userInputSplit = userInput.Split();
            try
            {
                switch (userInputSplit[0].ToLower())
                {
                    case "load":
                        LoadXmlFile(userInputSplit);
                        break;
                    case "save":
                        SaveXmlFile();
                        break;
                    case "setting":
                    case "--s":
                        ProcessSettingCommand(userInputSplit);
                        break;
                    default:
                        Console.WriteLine("Invalid Command.");
                        break;
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("not enough arguments");
            }
            catch (InvalidDataException)
            {
                Console.WriteLine("the given data type was incorrect.");
            }
        }

        private static void LoadXmlFile(string[] userInputSplit)
        {
            var path = userInputSplit[1];
            if (path == null) throw new InvalidDataException();
            if (File.Exists(path))
                _activitys = new Activitys(userInputSplit[1]);
            else
                Warnings.FileNotFound(path);
        }

        private static void SaveXmlFile()
        {
            if (_xmlPath == null)
            {
                Errors.NoFileLoaded();
                return;
            }

            _activitys.SaveToXml();
        }

        private static void ProcessSettingCommand(string[] userInputSplit)
        {
            string task = userInputSplit[1];
            switch (task)
            {
                case "edit":
                case "-e":
                    EditSetting(userInputSplit);
                    break;
                case "list":
                case "-l":
                    ListSetting(userInputSplit);
                    break;
            }
        }

        private static void ListSetting(string[] userInputSplit)
        {
            var setting = userInputSplit[2];
            switch (setting)
            {
                case "*":
                case "all":
                    foreach (var kvp in UserSettings.Settings)
                    {
                        Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                    }
                    break;
                default:
                    Console.WriteLine(UserSettings.Settings[setting]);
                    break;
            }
        }

        private static void EditSetting(string[] userInputSplit)
        {
            var setting = userInputSplit[2];
            switch (setting)
            {
                case "ShowWarnings":
                    UserSettings.ShowWarnings = Convert.ToBoolean(userInputSplit[3]);
                    break;
                case "ShowErrors":
                    UserSettings.ShowErrors = Convert.ToBoolean(userInputSplit[3]);
                    break;
                case "LinesBeforeUser":
                    UserSettings.LinesBeforeUser = Convert.ToInt32(userInputSplit[3]);
                    break;
                case "LinesAfterUser":
                    UserSettings.LinesBeforeUser = Convert.ToInt32(userInputSplit[3]);
                    break;
                default:
                    Warnings.SettingNotValid(setting);
                    break;
            }
        }
    }
}