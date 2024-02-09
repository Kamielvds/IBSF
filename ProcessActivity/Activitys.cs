using System;
using System.Linq;
using Commands;
using Commands.DataProcessor;
using ScoreHandeling;

namespace ProcessActivity
{
    public class Activitys
    {
        public Activitys(string XmlLocation)
        {
            Path = XmlLocation;
            Propeties = new Properties(Path, "xml");
        }

        private string _path;
        private Properties _properties;
        private AllScores _scores = new AllScores();

        public AllScores Scores
        {
            get => _scores;
            set => _scores = value;
        }

        public string Path
        {
            get => _path;
            set => _path = value;
        }

        public Properties Propeties
        {
            get => _properties;
            set => _properties = value;
        }

        public void LoadAll()
        {
            if (_properties == null) return;
            var xml = new Xml(_properties);
            var xmlReader = new XmlReader(xml);
            _scores = xmlReader.LoadScores();
        }

        public void AddActivity(Location location)
        {
            if (!Scores.LocationExists(location))
                _scores.AddLocation(location);
            else
                foreach (var scoresLocation in from scoresLocation in _scores.Locations
                         where scoresLocation.Name == location.Name
                         from score in location.Scores
                         select scoresLocation)
                    scoresLocation.AddScores(location.Scores);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}