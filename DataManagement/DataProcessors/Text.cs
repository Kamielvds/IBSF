using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Scores;

namespace Commands.DataProcessors
{
    /// <summary>
    /// Reader for text files.
    /// </summary>
    public class TextReader : Properties
    {
        public TextReader(string filePath) : base(filePath, "txt")
        {
            SetPath(filePath);
        }

        // used for getting the Command and it's value
        private string[] LineSplit => _line.Split(':');

        // shorter notation 
        private string Command => LineSplit[0];
        private string Value => LineSplit[1];

        // could make a property to readline whenever called, but needs a field for reader, which could cause issues
        // with disposing and thus resource management would be poor 
        private string _line;

        /// <summary>
        /// Retrieves all the data from the text file.
        /// </summary>
        /// <returns>
        /// returns the data in the AllScores class, for more info on how data is stored, check documentation
        /// </returns>
        public AllScores ReadFile()
        {
            if (ValidPath == false) return new AllScores();

            var allScores = new AllScores();
            var streamReader = new StreamReader(FilePath);
            var score = new Score();

            string locationName = null;
            var timeSeparator = "Hours";
            var distanceType = "Kilometers";
            // this is kept in this scope, so we can save a cycle by not always reading the same locations in a row

            while ((_line = streamReader.ReadLine()) != null)
            {
                switch (Command)
                {
                    // to speed up could use :: so the size of the array will be 3, resulting in easier access to instructions such as :end or :split*
                    case "splits":
                        while ((_line = streamReader.ReadLine()) != "splits:*")
                        {
                            var split = new Score.Split();
                            while ((_line = streamReader.ReadLine()) != "split:-")
                            {
                                switch (Command)
                                {
                                    case "time":
                                        split.Time = Convert.ToDouble(Value);
                                        break;
                                    case "distance":
                                        split.Distance = Convert.ToDouble(Value, CultureInfo.InvariantCulture);
                                        break;
                                    // ReSharper disable once StringLiteralTypo
                                    case "timeseparator":
                                        timeSeparator = Value;
                                        break;
                                    // ReSharper disable once StringLiteralTypo
                                    case "distanceunit":
                                        distanceType = Value;
                                        break;
                                    case "pace":
                                        split.Pace = Convert.ToDouble(Value);
                                        break;
                                }
                            }

                            split.DistanceUnit = distanceType;
                            split.TimeUnit = timeSeparator;
                            score.Splits.Add(split);
                        }

                        break;
                    case "age":
                        score.Age = Convert.ToInt32(Value);
                        break;
                    case "gender":
                        score.Gender = Convert.ToChar(Value);
                        break;
                    case "nationality":
                        score.Nationality = Value;
                        break;
                    case "date":
                        score.Date = Value;
                        break;
                    case "submitted":
                        score.Submitted = Convert.ToBoolean(Value);
                        break;
                    case "name":
                        score.Name = Value;
                        break;
                    case "track":
                        locationName = Value;
                        break;
                    case "pace":
                        score.Pace = Convert.ToDouble(Value);
                        break;
                    case "act":
                        switch (Value)
                        {
                            // Activity fixes
                            case "endl":
                                allScores.AddLocation(new Location(locationName, score));
                                score = new Score(); // renew after activity is ended
                                break;
                        }

                        break;
                }
            }

            streamReader.Close();
            return allScores;
        }
    }

    /// <summary>
    /// used for writing scores to the text file.
    /// </summary>
    public class TextWriter : Properties
    {
        public TextWriter(string filePath) : base(filePath, "txt")
        {
            SetPath(filePath);
        }

        /// <summary>
        /// removes pace, efficiently stores all scores
        /// </summary>
        /// <param name="allScores">
        /// all the scores to be written to the stream
        /// </param>
        /// <param name="deleteOriginal">
        /// remove the currently loaded file 
        /// </param>
        public string CompressText(AllScores allScores, bool deleteOriginal = false)
        {
            // could sort by date, which could remove a lot of lines, and separate the genders since there is only a possibility for 2 different 
            // genders.
            if (deleteOriginal)
                File.Delete(FilePath);
            var streamWriter = new StreamWriter(FilePath.Substring(0,FilePath.Length-4)+"Compressed.txt");
            
            foreach (var location in allScores.Locations)
            {
                streamWriter.WriteLine($"track:{location.Name}");
                foreach (var score in location.Scores)
                {
                    foreach (var item in score.AllObjects)
                    {
                        switch (item.Value)
                        {
                            case null:
                                streamWriter.WriteLine($"{item.Key}:");
                                break;
                            case string _:
                                if(item.Key != "pace")
                                    streamWriter.WriteLine($"{item.Key}:{item.Value}");
                                break;
                            default:
                            {
                                streamWriter.WriteLine("splits:*");
                                foreach (Score.Split split in (List<Score.Split>)item.Value)
                                {
                                    double[] types = ReadTimeSeparator(split);
                                    streamWriter.WriteLine("split:-");
                                    streamWriter.WriteLine($"time:{split.Time/types[0]}");
                                    streamWriter.WriteLine($"distance:{split.Distance/types[1]}");
                                    streamWriter.WriteLine("split:-");
                                }

                                streamWriter.WriteLine("splits:*");
                                break;
                            }
                        }
                    }

                    streamWriter.WriteLine("act:endl");
                }
            }

            streamWriter.Close();
            return FilePath.Substring(0, FilePath.Length - 4) + "Compressed.txt";
        }

        /// <summary>
        /// Rewrites all scores inside of a text file. This is the safest but slowest method.
        /// </summary>
        /// <param name="allScores">
        /// the scores that need to be written to the file
        /// </param>
        /// <param name="copy">
        /// copy the old file (safest)
        /// </param>
        public void RewriteText(AllScores allScores, bool copy = true)
        {
            if (copy) CopyFile();
            File.Delete(FilePath);
            var streamWriter = new StreamWriter(FilePath);

            foreach (var location in allScores.Locations)
            {
                streamWriter.WriteLine($"track:{location.Name}");
                foreach (var score in location.Scores)
                {
                    foreach (var item in score.AllObjects)
                    {
                        switch (item.Value)
                        {
                            case null:
                                streamWriter.WriteLine($"{item.Key}:");
                                break;
                            case string _:
                                streamWriter.WriteLine($"{item.Key}:{item.Value}");
                                break;
                            default:
                            {
                                streamWriter.WriteLine("splits:*");
                                foreach (Score.Split split in (List<Score.Split>)item.Value)
                                {
                                    streamWriter.WriteLine("split:-");
                                    // ReSharper disable once StringLiteralTypo
                                    streamWriter.WriteLine($"distanceunit:{split.DistanceUnit}");
                                    // ReSharper disable once StringLiteralTypo
                                    streamWriter.WriteLine($"timeseparator:{split.TimeUnit}");
                                    streamWriter.WriteLine($"distance:{split.Distance}");
                                    streamWriter.WriteLine($"time:{split.Time}");
                                    streamWriter.WriteLine($"pace:{split.Pace}");
                                    streamWriter.WriteLine("split:-");
                                }

                                streamWriter.WriteLine("splits:*");
                                break;
                            }
                        }
                    }

                    streamWriter.WriteLine("act:endl");
                }
            }

            streamWriter.Close();
        }
    }
}