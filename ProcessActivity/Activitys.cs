using System;
using System.Collections.Generic;
using System.Linq;
using Commands;
using Commands.DataProcessor;
using Scores;

namespace ProcessActivity
{
    public class Activitys
    {
        /// <summary>
        /// sets the location of the file, aswe"ll as loads all the scores
        /// </summary>
        /// <param name="fileLocation">
        /// the location fo th
        /// </param>
        /// <param name="lang">
        /// the language of the file, check documentation for all the supported filetypes
        ///     <default value="xml">
        ///     The default value is xml
        ///     </default>
        /// </param>
        public Activitys(string fileLocation, string lang = "xml")
        {
            Path = fileLocation;
            Scores = new AllScores();
            Lang = lang;
            LoadAll();
        }

        private Location            _localLocation;
        private List<Score>         _localScores;
        private Score               _localScore;
        private List<Score.Split>   _localSplits;
        private Score.Split         _localSplit;

        public AllScores Scores { get; set; }

        private string Path { get; set; }

        public string Lang { get; private set; }

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
            foreach (var location in Scores.Locations.Where(location => location.Name == name))
            {
                location.AddScore(_localScores);
                return;
            }

            // if no match was found make new location
            Scores.AddLocation(_localLocation);
        }

        public void CreateScore(string name, int age, string nationality, bool submitted, DateTime dateTime,
            char gender, string note, List<Score.Split> splits = null)
        {
            ClearLocalScore();
            _localScore.Splits      = splits ?? _localSplits;    // null check 
            _localScore.Name        = name;
            _localScore.Age         = age;
            _localScore.Nationality = nationality;
            _localScore.Submitted   = submitted;
            _localScore.Date        = dateTime;
            _localScore.Gender      = gender;
            _localScore.Note        = note;
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
            _localLocation  = new Location(null);
            _localScores    = new List<Score>();
            _localScore     = new Score();
            _localSplits    = new List<Score.Split>();
            _localSplit     = new Score.Split();
        }

        public void RemoveScore(int index)
        {
            _localScores.RemoveAt(index);
        }

        public void SaveFile()
        {
            switch (Lang)
            {
                case "xml":
                    SaveToXml();
                    break;
                case "txt":
                    SaveToTxt();
                    break;
            }
        }

        private void SaveToTxt()
        {
        }

        private void SaveToXml()
        {
            var reader = new XmlWriter(Path);
            reader.RewriteXml(Scores);
        }

        /// <summary>
        /// Loads all the scores using the properties file
        /// </summary>
        private void LoadAll()
        {
            if (Path == null) return;
            switch (Lang)
            {
                case "xml":
                    var xmlReader = new XmlReader(Path);
                    Scores = xmlReader.LoadXml();
                    break;
                case "txt":
                    var textReader = new TextReader(Path);
                    Scores = textReader.ReadFile();
                    break;
            }
        }
    }
}