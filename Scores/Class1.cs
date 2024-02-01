using System.Collections.Generic;

namespace Scores
{
    public class Score
    {
        
    }

    class Split
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

        public void AddSplit(double time, double distance)
        {
            _distances.Add(distance);
            _times.Add(time);
        }
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
    }
}