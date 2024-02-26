using System.Collections.Generic;
using System.IO;

namespace Commands
{
    
    /// <summary>
    /// Properties class for all readers and writers
    /// </summary>
    public abstract class Properties
    {
        protected Properties(string filePath, string lang)
        {
            _filePath = filePath;
            _lang = lang;
        }

        private string _filePath;
        private string _lang;
        
        public string FilePath
        {
            get => _filePath;
            set => _filePath = value;
        }
        public string Lang
        {
            get => _lang;
            set => _lang = value;
        }
        
        public bool ValidPath { get; private set; }
        
        private bool CheckPath(string path)
        {
            if (path == null || !File.Exists(path)) return false;
            ValidPath = true;
            return true;
        }

        public void SetPath(string path)
        {
            if (CheckPath(path))
            {
                FilePath = path;
                ValidPath = true;
            }
            else
                ValidPath = false;
        }
        
        /// <summary>
        /// check if the file exists and path isn't null
        /// </summary>
        /// <returns></returns>
        protected bool CheckPath()
        {
            if (FilePath == null || !File.Exists(FilePath))
            {
                ValidPath = false;
                return false;
            }

            ValidPath = true;
            return true;
        }
    }
}