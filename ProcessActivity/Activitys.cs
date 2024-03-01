using System;
using System.Collections.Generic;
using Commands.DataProcessor;
using Exceptions;
using Scores;

namespace ProcessActivity
{
    public class Activitys
    {
        /// <summary>
        /// sets the location of the file, aswe"ll as loads all the scores
        /// </summary>
        /// <param name="fileLocation">
        /// the location of the file
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
            ClearAllLocal();    // so all vars are initialised
            Lang = lang;
            LoadAll();
        }
        
        private Location            _localLocation;
        private List<Score>         _localScores;
        private Score               _localScore;
        private List<Score.Split>   _localSplits;
        private Score.Split         _localSplit;

        public AllScores Scores { get; set; }

        private string Path { get; }

        private string Lang { get; }

        private void AppendScore()
        {
            _localScores.Add(_localScore);
            ClearLocalScore();
        }

        private void AppendLocation()
        {
            Scores.AddLocation(_localLocation);
            ClearAllLocal();
        }
        
        private void AppendSplit()
        {
            _localSplits.Add(_localSplit);
            ClearLocalSplit();
        }

        public void CreateLocation(string name)
        {
            _localLocation = new Location(name, _localScores);
            AppendLocation();
        }
        
        public void CreateScore(string name, int age, string nationality, bool submitted, DateTime dateTime,
            char gender, string note, List<Score.Split> splits = null)
        {
            _localScore.Splits      = splits ?? _localSplits;    // null check -> local splits
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
            throw new InvalidScoreException("Not all fields entred correctly.");
        }
        
        /// <summary>
        /// Creating a split and appending it to the local list of all the splits.
        /// </summary>
        /// <param name="time">
        /// The time of the split in ms
        /// </param>
        /// <param name="distance">
        /// the distance of the split in a double precision 
        /// </param>
        public void CreateSplit(long time, double distance)
        {
            _localSplit.Time = time;
            _localSplit.Distance = distance;
            AppendSplit();
        }
        
        /// <summary>
        /// Removes a score
        /// </summary>
        /// <param name="index">
        /// the index of the Score to be removed
        /// </param>
        public void RemoveScore(int index)
        {
            _localScores.RemoveAt(index);
        }

        /// <summary>
        /// Clears the local location
        /// </summary>
        public void ClearLocalLocation()
        {
            _localLocation = new Location(null, new List<Score>());
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

        /// <summary>
        /// used fot thr constructor and when a score is added to the all scores, as a extra precaution
        /// </summary>
        private void ClearAllLocal()
        {
            _localLocation  = new Location(null,new List<Score>());
            _localScores    = new List<Score>();
            _localScore     = new Score();
            _localSplits    = new List<Score.Split>();
            _localSplit     = new Score.Split();
        }

        /// <summary>
        /// Save Scores to the desired Language type
        /// </summary>
        public void SaveFile(string lang = null)
        {
            if (lang == null) lang = Lang;
            switch (lang)
            {
                case "xml":
                    SaveToXml();
                    break;
                case "txt":
                    SaveToTxt();
                    break;
            }
        }

        /// <summary>
        /// Save the Scores to txt
        /// </summary>
        private void SaveToTxt()
        {
            var writer = new TextWriter(Path);
            writer.RewriteText(Scores);
        }

        /// <summary>
        /// Save the Scores to xml
        /// </summary>
        private void SaveToXml()
        {
            var writer = new XmlWriter(Path);
            writer.RewriteXml(Scores);
        }

        /// <summary>
        /// Loads all the scores using the properties class
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