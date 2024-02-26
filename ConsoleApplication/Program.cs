using System;
using System.IO;
using ConsoleApplication.Problems;
using ProcessActivity;
using static System.String;

namespace ConsoleApplication
{
    internal class Program
    {
        private static bool _running = true;
        private static string userInput;
        private static string _xmlPath;
        private static Activitys _activitys;
        private static string[] UserInputSplit => userInput.Split(' ');

        private static int InputLength => UserInputSplit.Length;

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

        /// <summary>
        /// Loading the sports file, and saving it to _activity's
        /// </summary>
        /// <exception cref="InvalidDataException">
        /// thrown whenever no path is given
        /// </exception>
        private static void LoadFile()
        {
            string path = null;
            if (InputLength > 1) path = UserInputSplit[1];
            if (path == null) throw new InvalidDataException();
            var lang = Empty;
            if (File.Exists(path))
            {
                _activitys = InputLength > 2 ? new Activitys(path, UserInputSplit[2]) : new Activitys(path);
            }
            else
                Warnings.FileNotFound(path);
        }

        private static void SaveFile()
        {
            if (_xmlPath == null)
            {
                Errors.NoFileLoaded();
                return;
            }

            _activitys.SaveFile();
        }

        private static void ProcessSettingCommand()
        {
            string task = UserInputSplit[1];
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
            var setting = UserInputSplit[2];
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
            var setting = UserInputSplit[2];
            switch (setting)
            {
                case "ShowWarnings":
                    UserSettings.ShowWarnings = Convert.ToBoolean(UserInputSplit[3]);
                    break;
                case "ShowErrors":
                    UserSettings.ShowErrors = Convert.ToBoolean(UserInputSplit[3]);
                    break;
                case "LinesBeforeUser":
                    UserSettings.LinesBeforeUser = Convert.ToInt32(UserInputSplit[3]);
                    break;
                case "LinesAfterUser":
                    UserSettings.LinesBeforeUser = Convert.ToInt32(UserInputSplit[3]);
                    break;
                default:
                    Warnings.SettingNotValid(setting);
                    break;
            }
        }
    }
}