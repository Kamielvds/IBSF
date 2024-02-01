using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Commands.DataProcessor.XML
{
    public class XmlWriter
    {
        public XmlWriter(Properties properties)
        {
            SetXmlPath(properties.FilePath);
        }

        private string _xmlPath;

        public string XmlPath => _xmlPath;

        #region Private Methods

        private void SetXmlPath(string path)
        {
            if (File.Exists(path) && path != null) {_xmlPath = path; return;}
            Console.WriteLine("EX: xml-nf");
            _xmlPath = null;
        }

        #endregion
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
            xmlDoc.Save(_xmlPath);
            AddNodeAndValue($"{location}/{mainNode}", names, values);
        }
    }
}