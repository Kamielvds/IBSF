using System;
using System.Collections.Generic;
using static DataProcessing.Program;
using static Filtering.Filters;

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
                // show -> unusable in form
                case "--s":
                case "show":
                    ShowFiltredList(command);
                    break;
            }
        }

        private static void ShowFiltredList(string[] command)
        {
            var option = command[2];
            switch (option)
            {
                case null:
                    //show all
                    break;
                case "--g":
                case "gender":
                    foreach (var i in filtred)
                    {
                        Console.WriteLine(Gender[i]);
                    }
                    break;
                case "--n":
                case "name":
                    foreach (var i in filtred)
                    {
                        Console.WriteLine(Gender[i]);
                    }
                    break;
            }
        }
    }
}