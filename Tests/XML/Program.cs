using System;
using System.Collections.Generic;
using System.Linq;
using Commands;
using Commands.DataProcessor;
using ProcessActivity;

namespace Tests
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var activitys = new Activitys("XML//XMLFiles/loading.xml");
            activitys.SaveFile();

            var act = new Activitys("TextFile/text.txt","txt");
        }
    }
}