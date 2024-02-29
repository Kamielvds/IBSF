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

        protected string FilePath { get; private set; }

        public string Lang { get; set; }

        public bool ValidPath { get; private set; }
        
        private bool CheckPath()
        {
            if (FilePath == null || !File.Exists(FilePath)) return false;
            ValidPath = true;
            return true;
        }

        /// <summary>
        /// used to change the path of the properties
        /// </summary>
        /// <param name="path">
        /// the path of the file
        /// </param>
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
        private bool SetFilePath(string filePath)
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
        
        /// <summary>
        /// used to backup userdata
        /// </summary>
        protected void CopyFile()
        {
            if(File.Exists(FilePath+"Copy"))
                File.Delete(FilePath+"Copy");
            File.Copy(FilePath, FilePath + "Copy");
        }
    }
}