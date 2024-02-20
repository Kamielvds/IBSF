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

        public static void InvalidFileType(string filetype)
        {
            if(UserSettings.ShowErrors)
                Console.WriteLine($"The file {filetype} is not supported");
        }

        public static void NoFileFound()
        {
            if(UserSettings.ShowErrors)
                Console.WriteLine("The file could not be found.");
        }

        public static void InvalidInstruction(ushort instruction, int location)
        {
            if (UserSettings.ShowErrors)
                Console.WriteLine($"The instruction {instruction} at value {location} is invalid.");
        }
    }
}