using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Commands;
using Commands.DataProcessor;
using ScoreHandeling;

namespace ProcessActivity
{
    public class Activitys
    {
        public Activitys(string xmlLocation)
        {
            Path = xmlLocation;
            Propeties = new Properties(Path, "xml"); // xml only
            Scores = new AllScores();
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

        public void AppendScore(Score score = null)
        {
            if (score != null)
                _localScores.Add(score);
            else
            {
                _localScores.Add(_localScore);
                ClearLocalScore();
            }
        }

        // i don't even like this
        public Score CreateScore(string name, int age, string nationality, bool submitted, DateTime dateTime,
            char gender, string note, List<Score.Split> splits = null, bool append = true)
        {
            if (append)
            {
                ClearLocalScore();
                _localScore.Splits = splits ?? _localSplits;
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
                return null;
            }
            var localScore = new Score();
            if (splits == null) goto argumentException;
            _localScore.Splits = splits;
            _localScore.Name = name;
            _localScore.Age = age;
            _localScore.Nationality = nationality;
            _localScore.Submitted = submitted;
            _localScore.Date = dateTime;
            _localScore.Gender = gender;
            _localScore.Note = note;
            if (_localScore.CheckValid()) return localScore;

            argumentException:
            throw new ArgumentException("Not all fields entred correctly.");
        }

        public Score.Split CreateSplit(long time, double distance, bool append = true)
        {
            if (!append) return new Score.Split(time, distance);
            _localSplit.Time = time;
            _localSplit.Distance = distance;
            _localSplits.Add(_localSplit);
            return null;
        }

        public void ClearLocalLocation()
        {
            _localLocation = new Location(null);
        }

        public void ClearLocalScores()
        {
            _localScores = new List<Score>();
        }

        public void ClearLocalScore()
        {
            _localScore = new Score();
        }

        public void ClearLocalSplits()
        {
            _localSplits = new List<Score.Split>();
        }

        public void ClearLocalSplit()
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

        public void LoadAll()
        {
            if (_properties == null) return;
            var xml = new Xml(_properties);
            var xmlReader = new XmlReader(xml);
            _scores = xmlReader.LoadScores();
        }
    }
}