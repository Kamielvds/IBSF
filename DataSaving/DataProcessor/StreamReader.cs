using System.IO;

namespace Commands.DataProcessor
{
    public abstract class Stream
    {
        public Stream(string filePath)
        {
            _path = filePath;
        }
        private string _path;

        /// <summary>
        /// check if the file exists and path isn't null
        /// </summary>
        /// <returns></returns>
        public bool FileExists()
        {
            return _path != null && File.Exists(_path);
        }
    }
    public class TextReader:Stream
    {
        public TextReader(string filePath):base(filePath)
        {
            FileExists();
        }
    }

    public class TextWriter:Stream
    {
        public TextWriter(string filePath):base(filePath)
        {
            FileExists();
        }
    }
}