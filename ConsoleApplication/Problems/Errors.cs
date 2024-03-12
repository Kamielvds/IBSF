using System;

namespace ConsoleApplication.Problems
{
    public static class Errors
    {
        public static void NoFileLoaded()
        {
            if (UserSettings.ShowErrors)
                Console.WriteLine("There is no file loaded, load one by using the load command");
        }

        public static void InvalidFileType(string filetype = "null")
        {
            if (UserSettings.ShowErrors) Console.WriteLine($"The file {filetype} is not supported");
        }

        public static void NoFileFound()
        {
            if (UserSettings.ShowErrors) Console.WriteLine("The file could not be found.");
        }

        public static void InvalidInstruction(byte instruction = 0xFF, int location = 0xFFFFFFF)
        {
            if (UserSettings.ShowErrors)
                Console.WriteLine($"The instruction {instruction} at value {location} is invalid.");
        }

        public static void InvalidInstruction(string command = "null", string instruction = "null")
        {
            if (UserSettings.ShowErrors)
                Console.WriteLine(
                    $"the given instruction: {instruction} of the command: {command} does not exist within this context");
        }

        public static void InvalidParameter(string instruction = "null", string parameter = "null")
        {
            if (UserSettings.ShowErrors)
                Console.WriteLine($"the given parameter: {parameter} of the instruction: {instruction} is invalid");
        }

        public static void InvalidDatatype()
        {
            if (UserSettings.ShowErrors) Console.WriteLine("the given data type was incorrect.");
        }

        public static void NotEnoughArguments()
        {
            if (UserSettings.ShowErrors) Console.WriteLine("not enough arguments");
        }

        public static void ElementNotFound()
        {
            if (UserSettings.ShowErrors) Console.WriteLine("The element in List was not found");
        }
        public static void InvalidScore()
        {
            if (UserSettings.ShowErrors) Console.WriteLine("The score is invalid");
        }
        public static void EmptyLocation()
        {
            if (UserSettings.ShowErrors) Console.WriteLine("The location is empty, initialise list first");
        }
        public static void EmptyActivity()
        {
            if (UserSettings.ShowErrors) Console.WriteLine("The activity's are empty.");
        }

        public static void NotFound(string s)
        {
            if (UserSettings.ShowErrors) Console.WriteLine($"The {s} was not found.");
        }

        public static void WrongFile(string targetLang, string actualLang = "none")
        {
            if (UserSettings.ShowErrors) Console.WriteLine($"The given file is {actualLang}, not {targetLang}");
        }
        
        public static void EmptyScore()
        {
            if (UserSettings.ShowErrors) Console.WriteLine($"the score is empty");
        }
    }
}