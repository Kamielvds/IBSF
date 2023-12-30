using System;
using System.Collections.Generic;
using static DataProcessing.Program;
using static Filtering.Filters;

namespace DataProcessing.commands
{
    public static class FilterCmd
    {
        public static List<int> Filtred;

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

                    Filtred = IndexFilterList(Gender, targetGender);
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

                    Filtred = IndexFilterList(Age, targetAge);
                    break;
                case "nationality":
                    var targetNationality = command[2];
                    if (targetNationality.Length != 2)
                    {
                        Console.WriteLine("Nationality should be a 2-char string.");
                        break;
                    }
                    Filtred = IndexFilterList(Nationality, targetNationality);
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
                        Console.WriteLine("Time should be a double.");
                        break;
                    }

                    Filtred = IndexFilterList(Time, targetTime);
                    break;
                case "name":
                case "--n":
                       var targetName = command[2];
                    
                    Filtred = IndexFilterList(Name, targetName);
                    break;
                
                // TODO show -> unusable in form rewrite for a listview
                case "--s":
                case "show":
                    ShowFiltredList(command);
                    break;
                default:
                    Console.WriteLine("No valid command was given");
                    break;
            }
        }

        // TODO implement all lists
        private static void ShowFiltredList(string[] command)
        {
            var option = command[2];
            switch (option)
            {
                case null:
                    // TODO show all
                    break;
                case "--n":
                case "name":
                    foreach (var i in Filtred)
                    {
                        Console.WriteLine(Name[i]);
                    }
                    break;
                case "--g":
                case "gender":
                    foreach (var i in Filtred)
                    {
                        Console.WriteLine(Gender[i]);
                    }
                    break;
                case "--t":
                case "time":
                    foreach (var i in Filtred)
                    {
                        Console.WriteLine(Time[i]);
                    }
                    break;
                case "--na":
                case "nationality":
                    foreach (var i in Filtred)
                    {
                        Console.WriteLine(Nationality[i]);
                    }
                    break;
                case "--a":
                case "age":
                    foreach (var i in Filtred)
                    {
                        Console.WriteLine(Age[i]);
                    }
                    break;
            }
        }
    }
}