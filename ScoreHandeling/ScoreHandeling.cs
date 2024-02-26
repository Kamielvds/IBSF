using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Scores
{
    public class AllScores
    {
        public List<Location> Locations { get; set; } = new List<Location>();

        public bool LocationExists(Location location)
        {
            return Locations.Any(loc => location.Name == loc.Name);
        }

        public bool LocationExists(string location)
        {
            return Locations.Any(loc => loc.Name == location);
        }

        public void AddLocation(Location location)
        {
            Locations.Add(location);
        }
    }

    public class Score
    {
        private string      _note;
        private string      _nationality;
        private string      _name;
        private DateTime    _date;
        private char        _gender;
        private int         _age;
        private bool        _submitted;
        private List<Split> _splits;

        public Dictionary<string, object> AllObjects =>
            new Dictionary<string, object>
            {
                { "note", _note },
                { "nationality", _nationality },
                { "name", _name },
                { "date", _date.ToString(CultureInfo.CurrentCulture) },
                { "gender", _gender.ToString() },
                { "age", _age.ToString() },
                { "submitted", _submitted.ToString() },
                { "splits", _splits }
            };

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

        public string Name
        {
            get => _name;
            set => _name = value;
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

        public List<Split> Splits
        {
            get => _splits;
            set => _splits = value;
        }

        public class Split
        {
            public Split()
            {
            }

            public Split(long time, double distance)
            {
                Time = time;
                Distance = distance;
            }

            private double _distance;
            private long _time;

            public double Distance
            {
                get => _distance;
                set => _distance = value;
            }

            public long Time
            {
                get => _time;
                set => _time = value;
            }
        }

        public bool CheckValid()
        {
            if (!(_gender == 'M' || _gender == 'F')) return false;
            if (_age < 13 || _age >= 99) return false;
            if (_name == null) return false;
            if (_splits == null) return false;
            return true;
        }
    }

    public class Location
    {
        public Location(string name, List<Score> scores = null)
        {
            Name = name;
            if (scores == null) return;
            foreach (var score in scores)
            {
                _scores.Add(score);
            }
        }

        private List<Score> _scores = new List<Score>();
        private string _name;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public List<Score> Scores
        {
            get => _scores;
            set => _scores = value;
        }

        public void AddScore(Score score)
        {
            _scores.Add(score);
        }

        public void AddScore(List<Score> scoresList)
        {
            foreach (var score in scoresList)
            {
                _scores.Add(score);
            }
        }
    }
}