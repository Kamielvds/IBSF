using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using ScoreHandeling;

namespace Commands.DataProcessor
{
    public class Xml
    {
        public Xml(Properties properties)
        {
            SetXmlPath(properties.FilePath);
            Properties = properties;
        }

        private Properties _properties;
        private string _xmlPath;

        public string XmlPath
        {
            get => _xmlPath;
            set => _xmlPath = value;
        }

        public Properties Properties
        {
            get => _properties;
            set => _properties = value;
        }

        private void SetXmlPath(string path)
        {
            if (File.Exists(path) && path != null)
            {
                _xmlPath = path;
                return;
            }

            Console.WriteLine("EX: xml-nf");
            _xmlPath = null;
        }
    }

    public class XmlReader
    {
        public XmlReader(Xml xml)
        {
            Xml = xml;
            XmlPath = xml.XmlPath;
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

        public AllScores LoadXml()
        {
            if (_xmlPath == null) return null;

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(_xmlPath);
            var root = xmlDoc.SelectSingleNode("/root");
            var tracksNode = root?.SelectSingleNode("tracks");
            var allScores = new AllScores();
            if (tracksNode == null) return allScores;
            foreach (XmlNode location in tracksNode)
            {
                // add location
                var newLocation = new Location(location.Name);
                allScores.AddLocation(newLocation);

                var newScores = new List<Score>();
                foreach (XmlNode score in location.ChildNodes)
                {
                    var newScore = new Score();
                    foreach (XmlNode element in score.ChildNodes)
                    {
                        switch (element.Name)
                        {
                            case "note":
                                newScore.Note = element.InnerText;
                                break;
                            case "nationality":
                                newScore.Nationality = element.InnerText;
                                break;
                            case "name":
                                newScore.Name = element.InnerText;
                                break;
                            case "date":
                                newScore.Date = Convert.ToDateTime(element.InnerText);
                                break;
                            case "gender":
                                newScore.Gender = Convert.ToChar(element.InnerText);
                                break;
                            case "age":
                                newScore.Age = Convert.ToInt32(element.InnerText);
                                break;
                            case "submitted":
                                newScore.Submitted = Convert.ToBoolean(element.InnerText);
                                break;
                            case "splits":
                                var newSplits = new List<Score.Split>();
                                foreach (XmlNode split in element.ChildNodes)
                                {
                                    var newSplit = new Score.Split();
                                    foreach (XmlNode splitElement in split)
                                    {
                                        switch (splitElement.Name)
                                        {
                                            case "time":
                                                newSplit.Time = Convert.ToDateTime(splitElement.InnerText);
                                                break;
                                            case "distance":
                                                newSplit.Distance = Convert.ToDouble(splitElement.InnerText);
                                                break;
                                        }
                                    }

                                    newSplits.Add(newSplit);
                                }

                                newScore.Splits = newSplits;
                                break;
                        }
                    }

                    newScores.Add(newScore);
                }

                newLocation.AddScores(newScores);
            }

            return allScores;
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
                         .Where(item => item.Attributes == null || item.Attributes[attribute].Value == attributeValue))
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
                }
                else
                    mainNodeElement.Add(new XElement(names[i], values[i]));
            }

            xmlDoc.Save(_xmlPath);
        }
    }
}