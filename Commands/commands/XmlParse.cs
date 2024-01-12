using System;

namespace Commands.commands
{
    public class XmlParse
    {
        public object ParseXmlElement(string elementName, string value)
        {
            return ParseXmlElement(elementName[0], value);
        }

        public object ParseXmlElement(char elementName, string value)
        {
            if (value.Length == 0) return null;
            switch (elementName)
            {
                case 'S':
                    return value;
                case 'C':
                    if (!(value.Length > 1)) return value[0];
                    var charArr = new char[value.Length];
                    for (var i = 0; i < value.Length; i++)
                    {
                        charArr[i] = value[i];
                    }

                    return charArr;
                case 'I':
                    return Convert.ToInt32(value);
                case 'B':
                    return Convert.ToByte(value);
                case 'D':
                    var doubleStr = value.Replace('.', ',');
                    return Convert.ToDouble(doubleStr);
            }

            return value;
        }
    }
}