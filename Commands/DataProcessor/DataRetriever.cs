using System.Collections.Generic;
using System.Linq;

namespace Commands.DataProcessor
{
    public class DataRetriever
    {
        public DataRetriever(Properties properties)
        {
            Properties = properties;
        }

        private Properties Properties{
            set => _properties = value;
            get => _properties;
        }
        
        private Properties _properties;

        public List<string> DifferentTracks()
        {
            return Properties.UserScores.Select(track => track.Key).ToList();
        }
    }
}