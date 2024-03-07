using System;

namespace ConsoleApplication
{
    public static class CustomMethods
    {
        /// <summary>
        /// support for 0-1 as a valid "falsy" or "truey" type. since default convert doesn't 
        /// </summary>
        /// <param name="s">
        /// the string that needs to be converted
        /// </param>
        /// <returns>
        /// wheather the value is true or false.
        /// </returns>
        public static bool CheckBoolean(string s)
        {
            switch (s)
            {
                case "0":
                    return false;
                case "1":
                    return true;
                default:
                    return Convert.ToBoolean(s);
            }
        }
    }
}