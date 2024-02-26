using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Scores;

namespace Commands.DataProcessor
{
    /// <summary>
    /// Reader for text files.
    /// </summary>
    public class TextReader : Properties
    {
        public TextReader(string filePath) : base(filePath,"txt")
        {
            CheckPath();
        }
        private string[] LineSplit => _line.Split(':');

        private string Command => LineSplit[0];

        private string Value => LineSplit[1];

        // could make a property to readline whenever called, but needs a field for reader, which could cause issues with disposing 
        private string _line;
        

        public AllScores ReadFile()
        {
            if (ValidPath == false) return new AllScores();

            var allScores       = new AllScores();
            var streamReader    = new StreamReader(FilePath);
            var scores          = new List<Score>();

            string locationName = null;

            while ((_line = streamReader.ReadLine()) != null)
            {
                var score = new Score();

                switch (Command)
                {
                    // to speed up could use :: so the size of the array will be 3, resulting in easier access to instructions such as :end or :split*
                    case "splits":
                        while ((_line = streamReader.ReadLine()) != "splits:*")
                        {
                            var splits = new List<Score.Split>();
                            if (_line == "split:-")
                            {
                                var split = new Score.Split();
                                while ((_line = streamReader.ReadLine()) != "split:-")
                                {
                                    switch (Command)
                                    {
                                        case "time":
                                            split.Time = Convert.ToInt64(Value);
                                            break;
                                        case "distance":
                                            split.Distance = Convert.ToDouble(Value, CultureInfo.InvariantCulture);
                                            break;
                                    }
                                }
                            }
                        }

                        break;
                }
            }

            return allScores;
        }
    }

    public class TextWriter : Properties
    {
        public TextWriter(string filePath) : base(filePath,"txt")
        {
            CheckPath();
        }
    }
}