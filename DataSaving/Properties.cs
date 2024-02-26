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
            FilePath = filePath;
            Lang = lang;
        }

        protected string FilePath { get; set; }

        public string Lang { get; set; }

        public bool ValidPath { get; private set; }
        
        private bool CheckPath()
        {
            if (FilePath == null || !File.Exists(FilePath)) return false;
            ValidPath = true;
            return true;
        }

        public void SetPath(string path)
        {
            if (SetFilePath(path))
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
        protected bool SetFilePath(string filePath)
        {
            if (FilePath == null || !File.Exists(filePath))
            {
                ValidPath = false;
                FilePath = filePath;
                return false;
            }

            ValidPath = true;
            return true;
        }
    }
}