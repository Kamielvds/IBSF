using System;
using System.IO;
using ConsoleApplication.Problems;
using ProcessActivity;

namespace ConsoleApplication
{
    internal class Program
    {
        private static bool _running = true;
        private static string _xmlPath;
        private static Activitys _activitys;
        private static string[] _userInputSplit;
        private static int InputLength => _userInputSplit.Length;


        public static void Main(string[] args)
        {
            LoadUserSettings();
            while (_running)
            {
                RequestUserInput();
            }
        }

        private static void LoadUserSettings()
        {
            // TODO make reader 
        }

        private static void RequestUserInput()
        {
            string userInput = Console.ReadLine();
            if (userInput == null) return;
            try
            {
                switch (_userInputSplit[0].ToLower())
                {
                    case "load":
                        LoadXmlFile();
                        break;
                    case "save":
                        SaveXmlFile();
                        break;
                    case "setting":
                    case "--s":
                        ProcessSettingCommand();
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

        private static void LoadXmlFile()
        {
            var path = _userInputSplit[1];
            if (path == null) throw new InvalidDataException();
            if (File.Exists(path))
                _activitys = new Activitys(_userInputSplit[1]);
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

        private static void ProcessSettingCommand()
        {
            string task = _userInputSplit[1];
            switch (task)
            {
                case "edit":
                case "-e":
                    EditSetting();
                    break;
                case "list":
                case "-l":
                    ListSetting();
                    break;
            }
        }

        private static void ListSetting()
        {
            var setting = _userInputSplit[2];
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

        private static void EditSetting()
        {
            var setting = _userInputSplit[2];
            switch (setting)
            {
                case "ShowWarnings":
                    UserSettings.ShowWarnings = Convert.ToBoolean(_userInputSplit[3]);
                    break;
                case "ShowErrors":
                    UserSettings.ShowErrors = Convert.ToBoolean(_userInputSplit[3]);
                    break;
                case "LinesBeforeUser":
                    UserSettings.LinesBeforeUser = Convert.ToInt32(_userInputSplit[3]);
                    break;
                case "LinesAfterUser":
                    UserSettings.LinesBeforeUser = Convert.ToInt32(_userInputSplit[3]);
                    break;
                default:
                    Warnings.SettingNotValid(setting);
                    break;
            }
        }
    }
}