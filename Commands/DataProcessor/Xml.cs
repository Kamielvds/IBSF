using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Commands.DataProcessor
{
    public class Xml
    {
        public Xml(string xmlPath)
        {
            SetXmlPath(xmlPath);
        }
        
        private Properties _properties;
        private string _xmlPath;

        public  string XmlPath
        {
            get => _xmlPath;
            set => _xmlPath = value;
        }

        public  Properties Properties
        {
            get => _properties;
            set => _properties = value;
        }

        private  void SetXmlPath(string path)
        {
            if (File.Exists(path) && path != null) {_xmlPath = path; return;}
            Console.WriteLine("EX: xml-nf");
            _xmlPath = null;
        }
    }
    public class XmlReader
    {
        public XmlReader(Xml xml)
        {
            Xml = xml;
            
        }

        private Xml _xml;
        private string _xmlPath;

        public Xml Xml
        {
            get => _xml;
            set => _xml = value;
        }

        public string XmlPath
        {
            get => _xmlPath;
            set => _xmlPath = value;
        }
        
        #region Public Methods

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
        #endregion
    }
    public class XmlWriter
    {
        public XmlWriter(Xml xml)
        {
            XmlPath = xml.XmlPath;
        }

        private string _xmlPath;


        public string XmlPath
        {
            set => _xmlPath = value;
        }

        /// <summary>
        /// Changes the Innertext of a given node, when the attribute from the given location is the same
        /// </summary>
        /// <param name="location"></param>
        /// Where to look for the attribute (in root)
        /// <param name="attribute"></param>
        /// the name of the attribute
        /// <param name="attributeValue"></param>
        /// the value of the attribute on which it is matched
        /// <param name="targetNode"></param>
        /// the node inside of the atrribute node that it need to find
        /// <param name="targetValue"></param>
        /// the target value for the node
        public void EditElementFromAttribute(string location, string attribute, string attributeValue,
            string targetNode, string targetValue)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(_xmlPath);
            XmlNode root = xmlDoc.SelectSingleNode("/root");
            XmlNode targetLocation = root?.SelectSingleNode(location);
            if (targetLocation == null) return;
            foreach (var book in targetLocation.Cast<XmlNode>()
                         .Where(item =>
                             item.Attributes == null || item.Attributes[attribute].Value == attributeValue))
            {
                foreach (var item in book.Cast<XmlNode>().Where(subItem => subItem.Name == targetNode))
                {
                    item.InnerText = targetValue;
                    break;
                }
            }
            xmlDoc.Save(_xmlPath);
        }
        
        /// <summary>
        /// Adds a Node, followed by more nodes and InnerText to an xml document
        /// </summary>
        /// <param name="location">The location for the node</param>
        /// <param name="mainNode">The name of the node where to place all the nodes</param>
        /// <param name="names">All the nodes that should be placed in the mainNode</param>
        /// <param name="values">All the InnerText to be added to the subnodes</param>
        public void AddNodeAndValue(string location, string mainNode, List<string> names, List<string> values)
        {
            var xmlDoc = XDocument.Load(_xmlPath);
            XElement element = xmlDoc.Root?.Element(location);
            if (element == null)
            {
                element = new XElement(location);
                xmlDoc.Root?.Add(element);
            }

            var mainNodeElement = new XElement(mainNode);
            element.Add(mainNodeElement);
            for (var i = 0; i < names.Count; i++)
            {
                if (names[i] == "id")
                {
                    mainNodeElement.Add(new XAttribute("id", values[i]));
                }else mainNodeElement.Add(new XElement(names[i], values[i]));
            }

            xmlDoc.Save(_xmlPath);
        }
    }
}