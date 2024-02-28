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
}