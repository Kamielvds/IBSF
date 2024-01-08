using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Commands.commands
{
    public class UserSettings
    {
        private static string xmlContent;

        public static void FilterCommand(string[] command)
        {
            switch (command[1])
            {
                case "document":
                case "--d":
                    SetXmlPath(command[2]);
                    break;
                case "load":
                case "--l":
                    LoadPath();
                    break;
                case "edit":
                case "--e": // settings, --e, ex.xml, root/ac/ac-1, name, Jeff
                    EditElement(command[2], command[3], command[4]);
                    break;
                default:
                    Console.WriteLine("EX: Filter-nf");
                    break;
            }
        }

        private static void SetXmlPath(string path)
        {
            if (File.Exists(path)) xmlContent = path;
            Console.WriteLine("EX: xml-nf");
        }

        private static void LoadPath()
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlContent);
            var root = xmlDoc.SelectSingleNode("/root");
            var tracksNode = root?.SelectSingleNode("tracks");
            var dictionary = new Dictionary<string, List<Dictionary<string, object>>>();

            // check all different tracks
            if (tracksNode == null) return;
            foreach (XmlNode trackNode in tracksNode.ChildNodes)
            {
                // Get the track name
                var trackName = trackNode.Name;
                var activities = new List<Dictionary<string, object>>();
                var activityProperties = new Dictionary<string, object>();
                // check all the activity's under the track
                foreach (XmlNode activityNode in trackNode.ChildNodes)
                {
                    // check if the node is an activity (other nodes mess up structure)
                    if (!activityNode.Name.StartsWith("activity-")) continue;

                    // check all the nodes in activity
                    foreach (XmlNode propertyNode in activityNode.ChildNodes)
                    {
                        // check if the node is "splits"
                        if (propertyNode.Name == "splits")
                        {
                            var splitsList = new List<Dictionary<string, string>>();

                            // check all children of the split
                            for (var i = 0; i < propertyNode.ChildNodes.Count; i++)
                            {
                                var splitNode = propertyNode.ChildNodes[i];
                                // check if the node is a split (based on the node name(other nodes mess up structure))
                                if (!splitNode.Name.StartsWith("split-")) continue;
                                // dictionary to store properties of the split (LINQ)(no idea how this works but it does (: )
                                var splitProperties = splitNode.ChildNodes.Cast<XmlNode>()
                                    .ToDictionary(splitPropertyNode => splitPropertyNode.Name,
                                        splitPropertyNode => splitPropertyNode.InnerText);
                                splitsList.Add(splitProperties);
                            }

                            activityProperties.Add(propertyNode.Name, splitsList);
                        }
                        else
                        {
                            activityProperties.Add(propertyNode.Name, propertyNode.InnerText);
                        }
                    }

                    activities.Add(activityProperties);
                }

                dictionary.Add(trackName, activities);
            }

            Properties.UserScores = dictionary;
        }

        private static void EditElement(string location, string targetNode, string value)
        {
            // load
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlContent);
            var nodes = xmlDoc.SelectNodes(location);
            if (nodes == null) return;
            foreach (XmlNode node in nodes)
            {
                if (node.Name != targetNode) return;
                node.Value = value;
            }
        }
    }
}