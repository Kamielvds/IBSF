using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Scores;

namespace Commands.DataProcessor
{
    

    public class XmlReader : Properties
    {
        /// <summary>
        /// default constructor XmlReader
        /// </summary>
        /// <param name="filePath">
        /// the path of the file
        /// </param>
        public XmlReader(string filePath):base(filePath,"xml")
        {
            SetPath(filePath);
        }
        
        /// <summary>
        /// loads all the scores
        /// </summary>
        /// <returns>
        /// returns the data in the AllScores class, for more info on how data is stored, check documentation
        /// </returns>
        public AllScores LoadXml()
        {
            if (!ValidPath) return null;

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(FilePath);
            
            // tries to find root-node and move to track node if not null
            var root = xmlDoc.SelectSingleNode("/root");
            var tracksNode = root?.SelectSingleNode("tracks");
            if (tracksNode == null) return null;
            
            // create object of AllScores
            var allScores = new AllScores();
            
            foreach (XmlNode location in tracksNode)
            {
                // add location
                var newLocation = new Location(location.Name, new List<Score>());
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
                                newScore.Date = element.InnerText;
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
                                                newSplit.Time = Convert.ToDouble(splitElement.InnerText);
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

                newLocation.AddScore(newScores);
            }

            return allScores;
        }
    }

    public class XmlWriter : Properties
    {
        /// <summary>
        /// default constructor XmlWriter
        /// </summary>
        /// <param name="filePath">
        /// the path of the file
        /// </param>
        public XmlWriter(string filePath):base(filePath,"xml")
        {
            SetPath(filePath);
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
            xmlDoc.Load(FilePath);
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

            xmlDoc.Save(FilePath);
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
            var xmlDoc = XDocument.Load(FilePath);
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

            xmlDoc.Save(FilePath);
        }

        // slow, but easiest
        /// <summary>
        /// deletes all the scoers from the file, and then tries to rewrite the whole document
        /// </summary>
        /// <param name="allScores">
        /// the value's from this class will be written to the xml document
        /// </param>
        /// <exception cref="DataException">
        /// if the file wasn't found or none is loaded
        /// </exception>
        public void RewriteXml(AllScores allScores)
        {
            // makes a backup copy just in case.
            CopyFile();
            
            // clear document
            if (!ValidPath) throw new DataException("The given path was wrong or not found.");
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(FilePath);
            xmlDoc.RemoveAll();

            // create xml declaration
            var xdoc = new XDocument { Declaration = new XDeclaration("1.0", "UTF-8", "true") };
            xdoc.Add(new XElement("root", null));
            xdoc.Save(FilePath);

            foreach (var location in allScores.Locations)
            {
                var locationContainer = new XElement(location.Name, null);
                for (var j = 0; j < location.Scores.Count; j++)
                {
                    var score = location.Scores[j];
                    var scoreContainer = new XElement("activity-" + j);
                    foreach (var item in score.AllObjects)
                    {
                        var itemContainer = new XElement(item.Key);
                        if (item.Value == null) continue;
                        if (item.Value.GetType() != typeof(string))
                        {
                            for (var i = 0; i < ((List<Score.Split>)item.Value).Count; i++)
                            {
                                var splitContainer = new XElement("split-" + i);
                                var split = ((List<Score.Split>)item.Value)[i];
                                splitContainer.Add(new XElement("distance", split.Distance));
                                splitContainer.Add(new XElement("time", split.Time));
                                itemContainer.Add(splitContainer);
                            }
                        }
                        else
                            itemContainer = new XElement(item.Key, item.Value);
                        scoreContainer.Add(itemContainer);
                    }

                    locationContainer.Add(scoreContainer);
                }

                xdoc.Root?.Add(locationContainer);
            }

            xdoc.Save(FilePath);
        }
    }
}