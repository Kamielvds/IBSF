using System;
using System.Collections.Generic;
using System.Linq;
using Commands;
using Commands.DataProcessor;

namespace Tests
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // creating Obj --> ok
            var properties = new Properties("/Users/kamielvds/Desktop/RiderC#/IBSF-2/Tests/XML/XMLFiles/loading.xml", "xml");
            var xmlProperties = new XmlReader(properties);
            // retrieving data xml --> ok, but should make a lib => done 
            /*
            var item = properties.UserScores.Keys.ElementAt(0);

            if (!properties.UserScores.ContainsKey(item)) return;
            var listOfDictionaries = properties.UserScores[item];

            foreach (var dictionary in listOfDictionaries)
            {
                foreach (var kvp in dictionary)
                {
                    if (kvp.Key == "splits" && kvp.Value is List<Dictionary<string, string>> list)
                    {
                        foreach (var split in list)
                        {
                            foreach (var splitKvp in split)
                            {
                                Console.WriteLine($"\t {splitKvp.Key}: {splitKvp.Value}");
                            }
                            Console.WriteLine("-----");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                    }
                }
                Console.WriteLine("-----");
            }
            */
            // data retriever -> ok
        }
    }
}