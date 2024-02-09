using System.Collections.Generic;

namespace Commands
{
    public class Properties
    {
        public Properties(string filePath, string lang)
        {
            _filePath = filePath;
            _lang = lang;
        }

        private string _filePath;
        private string _lang;

        private Dictionary<string, List<Dictionary<string, object>>> _userScores 
            = new Dictionary<string, List<Dictionary<string, object>>>();
        
        public string FilePath
        {
            get => _filePath;
            set => _filePath = value;
        }
        
        public Dictionary<string, List<Dictionary<string, object>>> UserScores
        {
            get => _userScores;
            set => _userScores = value;
        }

        public string Lang
        {
            get => _lang;
            set => _lang = value;
        }
    }
}