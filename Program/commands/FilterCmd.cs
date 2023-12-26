using System;
using System.Collections.Generic;
using Filtering;
using static DataProcessing.Program;
using static Filtering.Filters;
using static DataProcessing.Program;

namespace DataProcessing.commands
{
    public static class FilterCmd
    {
        public static List<int> filtred;

        public static void FilterCommand(string[] command)
        {
            switch (command[1])
            {
                case "gender":
                case "--g":
                    char targetGender;
                    try
                    {
                        targetGender = Convert.ToChar(command[2]);
                    }
                    catch
                    {
                        Console.WriteLine("Gender should be a char.");
                        break;
                    }

                    filtred = IndexFilterList(Gender, targetGender);
                    break;
                default:
                    Console.WriteLine("No valid command was given");
                    break;
            }
        }
    }
}