using System;
using System.Collections.Generic;

namespace ScoreHandeling
{
    public class AllScores
    {
        public AllScores()
        {
            _locations = new List<Location>();
        }
        private List<Location> _locations;

        public List<Location> Locations
        {
            get => _locations;
            set => _locations = value;
        }

        public void AddLocation(Location location)
        {
            _locations.Add(location);
        }
    }

    public class Scores
    {
        public Scores(List<Score> scores = null)
        {
            ScoreList = scores;
        }

        private List<Score> _scoreList;

        public List<Score> ScoreList
        {
            get => _scoreList;
            set => _scoreList = value;
        }

        public void AddScore(Score score)
        {
            _scoreList.Add(score);
        }
    }

    public class Score
    {
        private string _note;
        private string _nationality;
        private DateTime _date;
        private char _gender;
        private int _age;
        private bool _submitted;
        

        public string Note
        {
            get => _note;
            set => _note = value;
        }

        public string Nationality
        {
            get => _nationality;
            set => _nationality = value;
        }

        public DateTime Date
        {
            get => _date;
            set => _date = value;
        }

        public char Gender
        {
            get => _gender;
            set => _gender = value;
        }

        public int Age
        {
            get => _age;
            set => _age = value;
        }

        public bool Submitted
        {
            get => _submitted;
            set => _submitted = value;
        }
        
        
        
        public class Split
        {
            public Split(List<double> times = null, List<double> distances = null)
            {
                Times = times;
                Distances = distances;
            }

            private List<double> _distances;
            private List<double> _times;

            public List<double> Distances
            {
                get => _distances;
                set => _distances = value;
            }

            public List<double> Times
            {
                get => _times;
                set => _times = value;
            }

            /// <summary>
            /// Adds a split to the time and distance list
            /// </summary>
            /// <param name="time"></param>
            /// the time to be added
            /// <param name="distance"></param>
            /// the distance to be added
            public void AddSplit(double time, double distance)
            {
                _distances.Add(distance);
                _times.Add(time);
            }

            /// <summary>
            /// Adds a list of splits to the end of the time and distance list
            /// </summary>
            /// <param name="times"></param>
            /// the times to be added
            /// <param name="distances"></param>
            /// the distance to be added
            public void AddSplit(List<double> times, List<double> distances)
            {
                foreach (var time in times)
                {
                    _times.Add(time);
                }

                foreach (var distance in distances)
                {
                    _distances.Add(distance);
                }
            }

            /// <summary>
            /// edits the value of the time in the split
            /// </summary>
            /// <param name="index"></param>
            /// the index of the item to be edited
            /// <param name="time"></param>
            /// the new value of the time
            public void EditSplitTime(int index, double time)
            {
                _times[index] = time;
            }

            /// <summary>
            /// edits the value of the distance in the split
            /// </summary>
            /// <param name="index"></param>
            /// the index of the item to be edited
            /// <param name="distance"></param>
            /// the new value of the distance
            public void EditSplitDistance(int index, double distance)
            {
                _distances[index] = distance;
            }

            /// <summary>
            /// edits the value of the time in the split
            /// </summary>
            /// <param name="index"></param>
            /// the index of the item to be edited
            /// <param name="distance"></param>
            /// the new value of the distance
            /// <param name="time"></param>
            /// the new value of the time
            public void EditSplit(int index, double distance, double time)
            {
                _times[index] = time;
                _distances[index] = distance;
            }
        }
    }

    public class Location
    {
        public Location(string name, List<Scores> scores = null)
        {
            Name = name;
            if (scores != null) Scores = scores;
        }
        private List<Scores> _scores;
        private string _name;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public List<Scores> Scores
        {
            get => _scores;
            set => _scores = value;
        }

        public void AddScores(Scores scores)
        {
            _scores.Add(scores);
        }
    }
}