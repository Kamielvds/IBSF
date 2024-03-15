using System;
using ConsoleApplication.Problems;
// static
using static ConsoleApplication.CustomMethods;

namespace ConsoleApplication
{
    public static class Settings
    {
        private static string[] UserInputSplit => Program.UserInputSplit;

        /// <summary>
        /// process the settings command, whether to list a value or to edit one
        /// </summary>
        public static void ProcessSettingCommand()
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

        /// <summary>
        /// Listing the settings
        /// </summary>
        private static void ListSetting()
        {
            var setting = UserInputSplit[2];
            switch (setting)
            {
                case "*":
                case "all":
                    Console.WriteLine("-------------------------------------------");
                    foreach (var kvp in UserSettings.Settings)
                    {
                        Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                    }
                    Console.WriteLine("-------------------------------------------");

                    break;
                default:
                    try
                    {
                        Console.WriteLine(UserSettings.Settings[setting]);
                    }
                    catch (Exception)
                    {
                        // move to "catch row"
                        Errors.ElementNotFound();
                    }

                    break;
            }
        }

        /// <summary>
        /// Editing a setting
        /// </summary>
        private static void EditSetting()
        {
            var setting = UserInputSplit[2];
            switch (setting)
            {
                case "ShowWarnings":
                    UserSettings.ShowWarnings = CheckBoolean(UserInputSplit[3]);
                    break;
                case "ShowErrors":
                    UserSettings.ShowErrors = CheckBoolean(UserInputSplit[3]);
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