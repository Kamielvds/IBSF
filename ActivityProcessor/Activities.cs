using System;
using System.Collections.Generic;
using System.IO;
using Commands.DataProcessor;
using Exceptions;
using Scores;
using TextReader = Commands.DataProcessor.TextReader;
using TextWriter = Commands.DataProcessor.TextWriter;

namespace ProcessActivity
{
    public class Activities
    {
        /// <summary>
        /// sets the location of the file, as well as loads all the scores
        /// </summary>
        /// <param name="fileLocation">
        /// the location of the file
        /// </param>
        public Activities(string fileLocation)
        {
            Path = fileLocation;
            Scores = new AllScores();
            ClearAllLocal(); // so all vars are initialised
            Lang = fileLocation.Split('.')[1];
            LoadAll();
        }

        public Activities(string fileLocation, string lang)
        {
            Path = fileLocation;
            Scores = new AllScores();
            ClearAllLocal(); // so all vars are initialised
            Lang = lang;
            LoadAll();
        }

        private Location _localLocation;
        private List<Score> _localScores;
        private Score _localScore;
        private List<Score.Split> _localSplits;
        private Score.Split _localSplit;

        public AllScores Scores { get; private set; }

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

        /// <summary>
        /// uses the local scores to add scores to the list.
        /// </summary>
        /// <param name="name">
        /// the name of the location
        /// </param>
        public void CreateLocation(string name)
        {
            _localLocation = new Location(name, _localScores);
            AppendLocation();
        }

        public void CreateScore(string name, int age, string nationality, bool submitted, DateTime dateTime,
            char gender, string note, List<Score.Split> splits = null)
        {
            _localScore.Splits = splits ?? _localSplits; // null check -> local splits
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
            throw new InvalidScoreException("Not all fields entered correctly.");
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
            _localSplit.DistanceUnit = Score.DistanceSeparator.Meters.ToString();
            _localSplit.TimeUnit = Score.TimeSeparator.Minutes.ToString();
            AppendSplit();
        }

        /// <summary>
        /// creates a local split and appends it to the localSplits 
        /// </summary>
        /// <param name="time">
        /// the time of the split
        /// </param>
        /// <param name="distance">
        /// the distance of the split
        /// </param>
        /// <param name="timeSeparator">
        /// the time format of the split
        /// </param>
        /// <param name="distanceSeparator">
        /// the distance format 
        /// </param>
        public void CreateSplit(long time, double distance, Score.TimeSeparator timeSeparator, Score.DistanceSeparator distanceSeparator)
        {
            _localSplit.Time = time;
            _localSplit.Distance = distance;
            _localSplit.DistanceUnit = distanceSeparator.ToString();
            _localSplit.TimeUnit = timeSeparator.ToString();
            AppendSplit();
        }

        /// <summary>
        /// Creates splits and appends them
        /// </summary>
        /// <param name="times">
        /// all the times inside of a list
        /// </param>
        /// <param name="distances">
        /// all the distances inside of a list
        /// </param>
        /// <exception cref="UnequalSizeException">
        /// when the sizes of the two lists don't match
        /// </exception>
        public void CreateSplit(List<long> times, List<double> distances)
        {
            if (times.Count != distances.Count) throw new UnequalSizeException();
            for (var i = 0; i < times.Count; i++)
            {
                CreateSplit(times[i], distances[i]);
            }
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
        /// used for the constructor and when a score is added to the all scores, as a extra precaution
        /// </summary>
        private void ClearAllLocal()
        {
            _localLocation = new Location(null, new List<Score>());
            _localScores = new List<Score>();
            _localScore = new Score();
            _localSplits = new List<Score.Split>();
            _localSplit = new Score.Split();
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

            SetPace();
        }

        private void SetPace()
        {
            foreach (var location in Scores.Locations)
            {
                foreach (var score in location.Scores)
                {
                    double splitTime = 0;
                    double splitDistance = 0;

                    foreach (var split in score.Splits)
                    {
                        if (split == null) continue; 
                        double[] types = ReadTimeSeparator(split);
                        splitTime += split.Time / types[0];
                        splitDistance += split.Distance / types[1]; 
                    }

                    // km/h
                    score.Pace = splitDistance / splitTime;
                }
            }
        }

        private double[] ReadTimeSeparator(Score.Split split)
        {
            var types = new double[2];
            switch (split.TimeUnit)
            {
                case "Minutes":
                    types[0] = (double)Score.TimeSeparator.Minutes;
                    break;
                case "Hours":
                    types[0] = (double)Score.TimeSeparator.Hours;
                    break;
                case "Milliseconds":
                    types[0] = (double)Score.TimeSeparator.Milliseconds;
                    break;
                case "Seconds":
                    types[0] = (double)Score.TimeSeparator.Seconds;
                    break;
            }

            switch (split.DistanceUnit)
            {
                case "Kilometers":
                    types[1] = (double)Score.DistanceSeparator.Kilometers;
                    break;
                case "Meters":
                    types[1] = (double)Score.DistanceSeparator.Meters;
                    break;
            }

            return types;
        }
        
        public void LoadBackup()
        {
            if (File.Exists(Path.Substring(0,Path.Length-4)+$"Copy.{Lang}"))
            {
                throw new FileNotFoundException("There is no backup");
            }

            if (!File.Exists(Path)) throw new FileNotFoundException("There is no files loaded");
            File.Copy(Path.Substring(0,Path.Length-4)+$".{Lang}",Path, true);
            
            LoadAll();
        }
    }
}