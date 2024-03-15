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
            var act = new Activities("TextFile/text.txt","txt");
            act.SaveFile();
        }
    }
}