using System;
using System.Xml.Serialization;
using System.Xml;

namespace DataProcessing.commands

{
    public class UserSettings
    {
        public static void FilterCommand(string[] command)
        {
            switch (command[1])
            {
                case "path":
                case "--p":
                    UpdatePath(command[2]);
                    break;
            }
        }

        private static void UpdatePath(string xmlContent)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load("UserSettings/UserSettings.xml"); 

            var rootElement = xmlDoc.SelectSingleNode("/path");

            if (rootElement != null)
            {
                rootElement.InnerText = xmlContent;
                xmlDoc.Save("UserSettings/UserSettings.xml"); 
                Console.WriteLine("Path saved successfully");
            }
            else
            {
                Console.WriteLine("Root element 'path' not found, check xml file.");
            }
        }
    }
}