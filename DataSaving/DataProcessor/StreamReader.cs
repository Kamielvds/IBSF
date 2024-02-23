using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ScoreHandeling;

namespace Commands.DataProcessor
{
    public abstract class Stream
    {
        protected Stream(Properties properties)
        {
            Path = properties.FilePath;
        }

        protected string Path { get; set; }

        /// <summary>
        /// check if the file exists and path isn't null
        /// </summary>
        /// <returns></returns>
        protected bool CheckIntegrity()
        {
            return Path != null && File.Exists(Path);
        }
    }

    public class TextReader : Stream
    {
        public TextReader(Properties properties) : base(properties)
        {
            if(!CheckIntegrity()) Path = null;
        }

        public AllScores ReadFile()
        {
            if (Path == null) return new AllScores();
            var allScores = new AllScores();
            var streamReader = new StreamReader(Path);
            var scores = new List<Score>();
            string line;
            string track = null;

            while ((line = streamReader.ReadLine()) != null)
            {
                var score = new Score();
                while (line != "act::end" || line == null)
                {
                    var lineSplit = line.Split(':');
                    switch (lineSplit[0])
                    {
                        case "name":
                            score.Name = lineSplit[1];
                            break;
                        case "age":
                            score.Name = lineSplit[1];
                            break;
                        case "gender":
                            score.Name = lineSplit[1];
                            break;
                        case "nationality":
                            score.Name = lineSplit[1];
                            break;
                        case "date":
                            score.Name = lineSplit[1];
                            break;
                        case "submitted":
                            score.Name = lineSplit[1];
                            break;
                        case "track":
                            track = lineSplit[1];
                            break;
                        case "splits":
                            var splitList = new List<Score.Split>();
                            while (line != "split::*")
                            {
                                line = streamReader.ReadLine();
                                if (line != "split:-") continue;
                                line = streamReader.ReadLine();
                                var split = new Score.Split();
                                lineSplit = line.Split(':');
                                while (line != "split:-")
                                {
                                    switch (lineSplit[0])
                                    {
                                        case "distance":
                                            split.Distance = Convert.ToDouble(lineSplit[1]);
                                            break;
                                        case "time":
                                            split.Time = Convert.ToInt64(lineSplit[1]);
                                            break;
                                    }

                                    line = streamReader.ReadLine();
                                }

                                splitList.Add(split);
                            }

                            score.Splits = splitList;
                            break;
                    }
                    scores.Add(score);

                    line = streamReader.ReadLine();
                }

                
                if (track == null) continue;
                if (allScores.LocationExists(track))
                    foreach (var loc in allScores.Locations.Where(loc => loc.Name == track))
                        loc.AddScore(scores);
                else
                   allScores.AddLocation(new Location(track,scores));
                
                
            }
            
            track = null;
            return allScores;
        }
    }

    public class TextWriter : Stream
    {
        public TextWriter(Properties properties) : base(properties)
        {
            CheckIntegrity();
        }
    }
}