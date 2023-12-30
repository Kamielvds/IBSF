using System;
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
            // Create an XmlDocument object and load the XML string
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlContent);

            // Find the <path> element
            var pathNode = xmlDoc.SelectSingleNode("/path");
            
            if (pathNode != null)
            {
                // Get user input for the new path
                Console.Write("Enter the new path: ");
                var newPath = Console.ReadLine();

                // Update the content of the <path> element
                if (newPath != null) pathNode.InnerText = newPath;
            }
            else
            {
                Console.WriteLine("<path> element not found in XML.");
            }   
        }
    }
}