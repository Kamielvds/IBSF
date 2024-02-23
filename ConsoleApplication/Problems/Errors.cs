using System;

namespace ConsoleApplication.Problems
{
    public abstract class Errors
    {
        public static void NoFileLoaded()
        {
            if(UserSettings.ShowErrors)
                Console.WriteLine("There is no file loaded, load one by using the load command");
        }

        public static void InvalidFileType(string filetype = "null")
        {
            if(UserSettings.ShowErrors)
                Console.WriteLine($"The file {filetype} is not supported");
        }

        public static void NoFileFound()
        {
            if(UserSettings.ShowErrors)
                Console.WriteLine("The file could not be found.");
        }

        public static void InvalidInstruction(byte instruction = 0xFF, int location = 0xFFFFFFF)
        {
            if (UserSettings.ShowErrors)
                Console.WriteLine($"The instruction {instruction} at value {location} is invalid.");
        }
        public static void InstructionInvaid(string command = "null", string instruction = "null")
        {
            if (UserSettings.ShowErrors)
                Console.WriteLine($"the given instruction: {instruction} of the command: {command} does not exist within this context");
        }
        public static void ParameterInvaid(string instruction = "null", string parameter = "null")
        {
            if (UserSettings.ShowErrors)
                Console.WriteLine($"the given parameter: {parameter} of the instruction: {instruction} is invalid");
        }
    }
}