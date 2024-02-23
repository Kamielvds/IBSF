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
        public Activitys(string xmlLocation, string lang = "xml")
        {
            Path = xmlLocation;
            Propeties = new Properties(Path, lang); // xml default
            Scores = new AllScores();
            LoadAll();
        }

        private string _path;
        private Properties _properties;
        private AllScores _scores;

        private Location _localLocation;
        private List<Score> _localScores;
        private Score _localScore;
        private List<Score.Split> _localSplits;
        private Score.Split _localSplit;

        public AllScores Scores
        {
            get => _scores;
            set => _scores = value;
        }

        private string Path
        {
            get => _path;
            set => _path = value;
        }

        public Properties Propeties
        {
            get => _properties;
            set => _properties = value;
        }

        private void AppendScore(Score score = null)
        {
            if (score != null)
                _localScores.Add(score);
            else
            {
                _localScores.Add(_localScore);
                ClearLocalScore();
            }
        }

        public void CreateLocation(string name)
        {
            _localLocation = new Location(name, _localScores);
            foreach (var location in _scores.Locations.Where(location => location.Name == name))
            {
                location.AddScore(_localScores);
                return;
            }

            // if no match was found make new location
            _scores.AddLocation(_localLocation);
        }

        public void CreateScore(string name, int age, string nationality, bool submitted, DateTime dateTime,
            char gender, string note, List<Score.Split> splits = null)
        {
            ClearLocalScore();
            _localScore.Splits = splits ?? _localSplits;    // null check 
            _localScore.Name = name;
            _localScore.Age = age;
            _localScore.Nationality = nationality;
            _localScore.Submitted = submitted;
            _localScore.Date = dateTime;
            _localScore.Gender = gender;
            _localScore.Note = note;
            if (_localScore.CheckValid())
                AppendScore();
            else
                goto argumentException;
            return;

            argumentException:
            throw new ArgumentException("Not all fields entred correctly.");
        }

        public void CreateSplit(long time, double distance)
        { 
            _localSplit.Time = time;
            _localSplit.Distance = distance;
            _localSplits.Add(_localSplit);
            ClearLocalSplit();
        }

        public void ClearLocalLocation()
        {
            _localLocation = new Location(null);
        }

        public void ClearLocalScores()
        {
            _localScores = new List<Score>();
        }

        private void ClearLocalScore()
        {
            _localScore = new Score();
        }

        public void ClearLocalSplits()
        {
            _localSplits = new List<Score.Split>();
        }

        private void ClearLocalSplit()
        {
            _localSplit = new Score.Split();
        }

        public void ClearAllLocal()
        {
            _localLocation = new Location(null);
            _localScores = new List<Score>();
            _localScore = new Score();
            _localSplits = new List<Score.Split>();
            _localSplit = new Score.Split();
        }

        public void RemoveScore(int index)
        {
            _localScores.RemoveAt(index);
        }

        public void SaveToXml()
        {
            var reader = new XmlWriter(new Xml(_properties));
            reader.RewriteXml(_scores);
        }

        private void LoadAll()
        {
            if (_properties == null) return;
            switch (_properties.Lang)
            {
                case "xml":
                    var xmlReader = new XmlReader(new Xml(_properties));
                    _scores = xmlReader.LoadScores();
                    break;
                case "txt":
                    var textReader = new TextReader(_properties);
                    _scores = textReader.ReadFile();
                    break;
            }
        }
    }
}