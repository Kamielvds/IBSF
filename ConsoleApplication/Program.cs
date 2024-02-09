using System;
using System.Collections.Generic;
using Filtering;

namespace ConsoleApplication
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var list = new List<int> { 12, 435, 456, 5, 768 };
            Filters.SortDescending(ref list);
            foreach (var number in list)
            {
                Console.WriteLine(number);
            }
        }
    }
}