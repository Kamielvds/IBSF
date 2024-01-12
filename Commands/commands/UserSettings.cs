using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Commands.commands
{
    public class UserSettings
    {
        public UserSettings(string path)
        {
            _xmlPath = path;
        }

        private string _xmlPath;
        public string XmlPath => _xmlPath;

        public void SetXmlPath(string path)
        {
            if (File.Exists(path)) _xmlPath = path;
            Console.WriteLine("EX: xml-nf");
        }

        public Dictionary<string, List<Dictionary<string, object>>> LoadXml()
        {
            if (_xmlPath == null) return null;

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(_xmlPath);
            var root = xmlDoc.SelectSingleNode("/root");
            var tracksNode = root?.SelectSingleNode("tracks");
            var dictionary = new Dictionary<string, List<Dictionary<string, object>>>();

            // check all different tracks
            if (tracksNode == null) return null;
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

            return dictionary;
        }

        public void EditElement(string location, string targetNode, string value)
        {
            // load
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(_xmlPath);
            var nodes = xmlDoc.SelectNodes(location);
            if (nodes == null) return;
            foreach (XmlNode node in nodes)
            {
                if (node.Name != targetNode) return;
                node.Value = value;
            }
        }

        // if you need to rename in different location, use this method in foreach.
        public void RenameNode(string location, string targetNode, string name)
        {
            // load
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(_xmlPath);
            var nodes = xmlDoc.SelectNodes(location);
            if (nodes == null) return;
            foreach (XmlNode node in nodes)
            {
                if (node.Name != targetNode) return;
                node.Value = name;
            }
        }

        public void AddNodeAndValue(string location, string name, string value)
        {
            var xmlDoc = XDocument.Load(_xmlPath);
            var xElement = new XElement(name, value);
            xmlDoc.Element(location)?.Add(xElement);
            xmlDoc.Save(_xmlPath);
        }

        public void AddNodeAndValue(string location, string[] names, string[] values)
        {
            if (names == null) throw new ArgumentNullException(nameof(names));
            var xmlDoc = XDocument.Load(_xmlPath);
            var i = 0;
            foreach (var name in names)
            {
                xmlDoc.Element(location)?.Add(new XElement(name, values[i]));
                i++;
            }
            xmlDoc.Save(_xmlPath);
        }

        // used for creating a main node with sub-notes
        public void AddNodeAndValue(string location, string mainNode, string[] names, string[] values)
        {
            var xmlDoc = XDocument.Load(_xmlPath);
            xmlDoc.Element(location)?.Add(new XElement(mainNode));
            AddNodeAndValue($"{location}/{mainNode}", names, values);
        }
    }
}