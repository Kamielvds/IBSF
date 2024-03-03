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
    public class NotEnoughArguments : Exception
    {
        public NotEnoughArguments() : base("Not enough arguments were given")
        {
            HResult = -6675454;
        }

        public NotEnoughArguments(string s) : base(s)
        {
            HResult = -6675454;
        }

        public NotEnoughArguments(string s, Exception innerException) : base(s, innerException)
        {
            HResult = -6675454;
        }
    }

    [Serializable]
    public class InvalidArguments : Exception
    {
        public InvalidArguments() : base("the argument is invalid.")
        {
            HResult = -6675453;
        }

        public InvalidArguments(string s) : base(s)
        {
            HResult = -6675453;
        }

        public InvalidArguments(string s, Exception innerException) : base(s, innerException)
        {
            HResult = -6675453;
        }
    }

    [Serializable]
    public class UnequalSizeException : Exception
    {
        public UnequalSizeException() : base("the argument is invalid.")
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
    public class ScoreNotFound : Exception
    {
        public ScoreNotFound() : base("the argument is invalid.")
        {
            HResult = -6675451;
        }

        public ScoreNotFound(string s) : base(s)
        {
            HResult = -6675451;
        }

        public ScoreNotFound(string s, Exception innerException) : base(s, innerException)
        {
            HResult = -6675451;
        }
    }

    [Serializable]
    public class LocationNotFound : Exception
    {
        public LocationNotFound() : base("the argument is invalid.")
        {
            HResult = -6675451;
        }

        public LocationNotFound(string s) : base(s)
        {
            HResult = -6675451;
        }

        public LocationNotFound(string s, Exception innerException) : base(s, innerException)
        {
            HResult = -6675451;
        }
    }
}