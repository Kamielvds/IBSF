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
            var properties = new Properties(@"XML\XMLFiles\loading.xml", "xml");
            var xmlProperties = new XmlProperties(properties);
            // loading Xml --> ok
            var userData = xmlProperties.LoadXml();
            // retrieving data xml --> ok, but should make a lib
            var item = userData.Keys.ElementAt(0);

            if (!userData.ContainsKey(item)) return;
            var listOfDictionaries = userData[item];

            foreach (var dictionary in listOfDictionaries)
            {
                foreach (var kvp in dictionary)
                {
                    if (kvp.Key == "splits" && kvp.Value is List<Dictionary<string, string>> list)
                    {
                        foreach (var split in list)     
                            // splits numbers are deprecated in the loading, since they can be found with the index.
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


        }
    }
}