﻿using System;
using System.Collections.Generic;
using System.Linq;
using Commands;
using Commands.DataProcessor;
using ProcessActivity;
using ScoreHandeling;

namespace Tests
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // creating Obj --> ok
            var properties = new Properties("/Users/kamielvds/Desktop/RiderC#/IBSF-2/Tests/XML/XMLFiles/loading.xml", "xml");
            var xml = new Xml(properties);
            var xmlReader = new XmlReader(xml);

            var scores = xmlReader.LoadScores();

            var activitys = new Activitys("/Users/kamielvds/Desktop/RiderC#/IBSF-2/Tests/XML/XMLFiles/loading.xml");
            activitys.Dispose();
            activitys.AddScore(new Score());
        }
    }
}