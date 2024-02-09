using System;
using System.Collections.Generic;
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
            Scores = new AllScores();
        }

        private string _path;
        private Properties _properties;
        private AllScores _scores;

        /// <summary>
        /// a buffer for current scores
        /// </summary>
        private List<Score> _localScores;

        private Location _localLocation;

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

        public Location CreateLocation(bool returnValue, string name, List<Score> scores = null)
        {
            if (scores != null)
                if (returnValue)
                    return new Location(name, scores);
                else
                    CreateLocation(string name, List < Score > scores);
            if (!returnValue)
            {
                CreateLocation();
                return null;
            }

            var location = new Location(name, _localScores);
            // clear buffer so user can add new activity's
            ClearScoresBuffer();
            return location;
        }

        private void ClearScoresBuffer()
        {
            _localScores = new List<Score>();
        }

        public void CreateLocation(string name, List<Score> scores)
        {
            _localLocation = new Location(name, scores);
        }

        public void CreateLocation(string name)
        {
            _localLocation = new Location(name, _localScores);
        }

        public void ClearScores()
        {
            _localScores = new List<Score>();
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
                {
                    scoresLocation.AddScores(location.Scores);
                }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}