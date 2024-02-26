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
        public TextReader(string filePath) : base(filePath, "txt")
        {
            SetFilePath(filePath);
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
        //TODO Fix reader
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

                                score.Splits.Add(split);
                            }
                        }

                        break;
                    case "name":
                        score.Name = Value;
                        break;
                    //TODO Incapsulate all Types
                }
            }

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
            SetFilePath(filePath);
        }

        /// <summary>
        /// used to backup userdata
        /// </summary>
        public void CopyFile()
        {
            File.Copy(FilePath, FilePath + "Copy");
        }
    }
}