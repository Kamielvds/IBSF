using System;

namespace ConsoleApplication.Problems
{
    public abstract class Warnings
    {
        public static void CreatedFile(string file = "null")
        {
            if (UserSettings.ShowWarnings) Console.WriteLine($"The file {file} was not found, so it was created");
        }

        public static void WrongApp(string app = "null")
        {
            if (UserSettings.ShowWarnings) Console.WriteLine($"this file is made for {app}, not XAM");
        }

        public static void SettingNotValid(string setting = "null")
        {
            if (UserSettings.ShowWarnings) Console.WriteLine($"The setting {setting} is not valid, nothing was affected");
        }

        public static void SettingValueNotValid(string value = "null", string setting = "null")
        {
            if (UserSettings.ShowWarnings)
                Console.WriteLine($"The given value {value} for {setting} is not valid, no settings were changed.");
        }
        public static void FileNotFound(string file = "null")
        {
            if (UserSettings.ShowWarnings)
                Console.WriteLine($"the file at path {file} doesn't exists, no changes were made.");
        }
        public static void DirectoryNotFound(string file = "null")
        {
            if (UserSettings.ShowWarnings)
                Console.WriteLine($"the directory at path {file} doesn't exists, no changes were made.");
        }
        public static void DepricatedMemoryType(string type = "null")
        {
            if (UserSettings.ShowWarnings)
                Console.WriteLine($"the current program is a {type}, which is depricated, please consider using ");
        }

        public static void CommandNotFound(string command = "null")
        {
            if (UserSettings.ShowWarnings)
                Console.WriteLine($"the command {command} does not exist");
        }
    }
}