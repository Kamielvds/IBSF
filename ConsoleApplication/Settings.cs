using System;
using ConsoleApplication.Problems;

namespace ConsoleApplication
{
    public class Settings
    {
        private static string[] UserInputSplit => Program.UserInputSplit;
        
        /// <summary>
        /// process the settings command, wheather to list a value or to edit one
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
        public static void ListSetting()
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

        /// <summary>
        /// Editting a setting
        /// </summary>
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