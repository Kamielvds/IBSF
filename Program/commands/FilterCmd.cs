using System;
using Filtering;
using static DataProcessing.Program;
using static Filtering.Filters;
using static DataProcessing.Program;

namespace DataProcessing.commands
{
    public static class FilterCmd
    {
        public static void FilterCommand(string[] command)
        {
            switch (command[1])
            {
                case "gender":
                case "--g":
                    FilterByGender(command);
                    break;
            }
        }

        private static void FilterByGender(string[] command)
        {
            char targetGender;
            try
            {
                targetGender = Convert.ToChar(command[2]);
            }
            catch
            {
                Console.WriteLine("Genders are supposed to be Char's");
                return;
            }
            
            var filteredList = FilterList(Gender,targetGender);

            foreach (var gender in filteredList)
            {
                Console.WriteLine(gender);
            }
            
        }
    }
}