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
        private DateTime    _date;

        public Dictionary<string, object> AllObjects =>
            new Dictionary<string, object>
            {
                { "note", Note },
                { "nationality", Nationality },
                { "name", Name },
                { "date", _date.ToString(CultureInfo.CurrentCulture) },
                { "gender", Gender.ToString() },
                { "age", Age.ToString() },
                { "submitted", Submitted.ToString() },
                { "splits", Splits }
            };

        public string Note { get; set; }

        public string Nationality { get; set; }

        public string Name { get; set; }

        public DateTime Date
        {
            get => _date;
            set => _date = value;
        }

        public char Gender { get; set; }

        public int Age { get; set; }

        public bool Submitted { get; set; }

        public List<Split> Splits { get; set; }

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

            public double Distance { get; set; }

            public long Time { get; set; }
        }

        public bool CheckValid()
        {
            if (!(Gender == 'M' || Gender == 'F')) return false;
            if (Age < 13 || Age >= 99) return false;
            if (Name == null) return false;
            if (Splits == null) return false;
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
                Scores.Add(score);
            }
        }

        public string Name { get; set; }

        public List<Score> Scores { get; set; } = new List<Score>();

        public void AddScore(Score score)
        {
            Scores.Add(score);
        }

        public void AddScore(List<Score> scoresList)
        {
            foreach (var score in scoresList)
            {
                Scores.Add(score);
            }
        }
    }
}