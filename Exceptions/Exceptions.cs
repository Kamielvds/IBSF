using System;

namespace Exceptions
{
    [Serializable]
    public class EmptyLocationException : Exception
    {
        public EmptyLocationException()
            : base("The Location is empty.")
        {
            HResult = -6675467;
        }
        public EmptyLocationException(string s)
            : base(s)
        {
            HResult = -6675467;
        }
        public EmptyLocationException(string s,Exception innerException)
            : base(s,innerException)
        {
            HResult = -6675467;
        }
        
    }
    [Serializable]
    public class InvalidScoreException : Exception
    {
        public InvalidScoreException()
            : base("The Location is empty.")
        {
            HResult = -6675455;
        }
        public InvalidScoreException(string s)
            : base(s)
        {
            HResult = -6675455;
        }
        public InvalidScoreException(string s,Exception innerException)
            : base(s,innerException)
        {
            HResult = -6675455;
        }
        
    }
}