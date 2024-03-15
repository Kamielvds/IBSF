using System;

namespace Exceptions
{
    [Serializable]
    public class EmptyLocationException : Exception
    {
        public EmptyLocationException() : base("The Location is empty.")
        {
            HResult = -6675467;
        }

        public EmptyLocationException(string s) : base(s)
        {
            HResult = -6675467;
        }

        public EmptyLocationException(string s, Exception innerException) : base(s, innerException)
        {
            HResult = -6675467;
        }
    }

    [Serializable]
    public class EmptyActivityException : Exception
    {
        public EmptyActivityException() : base("The Activity is empty.")
        {
            HResult = -6675468;
        }

        public EmptyActivityException(string s) : base(s)
        {
            HResult = -6675468;
        }

        public EmptyActivityException(string s, Exception innerException) : base(s, innerException)
        {
            HResult = -6675468;
        }
    }

    [Serializable]
    public class InvalidScoreException : Exception
    {
        public InvalidScoreException() : base("The Score is invalid.")
        {
            HResult = -6675455;
        }

        public InvalidScoreException(string s) : base(s)
        {
            HResult = -6675455;
        }

        public InvalidScoreException(string s, Exception innerException) : base(s, innerException)
        {
            HResult = -6675455;
        }
    }

    [Serializable]
    public class NotEnoughArgumentsException : Exception
    {
        public NotEnoughArgumentsException() : base("Not enough arguments were given")
        {
            HResult = -6675454;
        }

        public NotEnoughArgumentsException(string s) : base(s)
        {
            HResult = -6675454;
        }

        public NotEnoughArgumentsException(string s, Exception innerException) : base(s, innerException)
        {
            HResult = -6675454;
        }
    }

    [Serializable]
    public class InvalidArgumentsException : Exception
    {
        public InvalidArgumentsException() : base("the argument is invalid.")
        {
            HResult = -6675453;
        }

        public InvalidArgumentsException(string s) : base(s)
        {
            HResult = -6675453;
        }

        public InvalidArgumentsException(string s, Exception innerException) : base(s, innerException)
        {
            HResult = -6675453;
        }
    }

    [Serializable]
    public class UnequalSizeException : Exception
    {
        public UnequalSizeException() : base("the sizes are unequal.")
        {
            HResult = -6675452;
        }

        public UnequalSizeException(string s) : base(s)
        {
            HResult = -6675452;
        }

        public UnequalSizeException(string s, Exception innerException) : base(s, innerException)
        {
            HResult = -6675452;
        }
    }

    [Serializable]
    public class ScoreNotFoundException : Exception
    {
        public ScoreNotFoundException() : base("the score was not found.")
        {
            HResult = -6675451;
        }

        public ScoreNotFoundException(string s) : base(s)
        {
            HResult = -6675451;
        }

        public ScoreNotFoundException(string s, Exception innerException) : base(s, innerException)
        {
            HResult = -6675451;
        }
    }

    [Serializable]
    public class LocationNotFoundException : Exception
    {
        public LocationNotFoundException() : base("the location was not found.")
        {
            HResult = -6675451;
        }

        public LocationNotFoundException(string s) : base(s)
        {
            HResult = -6675451;
        }

        public LocationNotFoundException(string s, Exception innerException) : base(s, innerException)
        {
            HResult = -6675451;
        }
    }
    
    [Serializable]
    public class EmptyScoreException : Exception
    {
        public EmptyScoreException() : base("the score is empty.")
        {
            HResult = -6675450;
        }

        public EmptyScoreException(string s) : base(s)
        {
            HResult = -6675450;
        }

        public EmptyScoreException(string s, Exception innerException) : base(s, innerException)
        {
            HResult = -6675450;
        }
    }
}