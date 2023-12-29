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
                case "age":
                case "--a":
                    byte targetAge;
                    try
                    {
                        targetAge = Convert.ToByte(command[2]);
                    }
                    catch
                    {
                        Console.WriteLine("Age should be a string.");
                        break;
                    }

                    filtred = IndexFilterList(Age, targetAge);
                    break;
                case "nationality":
                    string targetNationality;
                    try
                    {
                        targetNationality = command[2];
                    }
                    catch
                    {
                        Console.WriteLine("Nationalty should be a 2-character string.");
                        break;
                    }

                    filtred = IndexFilterList(Nationality, targetNationality);
                    break;
                case "time":
                case "--t":
                    double targetTime;
                    try
                    {
                        targetTime = Convert.ToDouble(command[2]);
                    }
                    catch
                    {
                        Console.WriteLine("Nationalty should be a 2-character string.");
                        break;
                    }

                    filtred = IndexFilterList(Time, targetTime);
                    break;
                // show -> unusable in form rewrite for a listview
                case "--s":
                case "show":
                    ShowFiltredList(command);
                    break;
                default:
                    Console.WriteLine("No valid command was given");
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