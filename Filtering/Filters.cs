using System;
using System.Collections.Generic;
using System.Linq;

namespace Filtering
{
    public class Filters
    {
        #region unregistred

        /*
         * used for filtering a List, without indexes
         *      returns => List<t_var>
         */

        public static List<string> FilterList(List<string> unfiltredArray, string criteria)
        {
            return unfiltredArray.Where(str => str == criteria).ToList();
        }

        public static List<int> FilterList(List<int> unfiltredArray, int criteria)
        {
            return unfiltredArray.Where(i => i == criteria).ToList();
        }

        public static List<double> FilterList(List<double> unfiltredArray, int criteria)
        {
            return unfiltredArray.Where(i => Math.Abs(i - criteria) < 0.00001)
                .ToList(); // using tolerance of 5 digits  
        }
        public static List<char> FilterList(List<char> unfiltredArray, char criteria)
        {
            return unfiltredArray.Where(i => i == criteria).ToList();
        }

        #endregion

        #region registerd

        /*
         * used for getting the address of a filtred list
         *      returns => List<int>
         */

        // filter string
        public static List<int> IndexFilterList(List<string> unfiltredArray, string criteria)
        {
            var indexList = new List<int>();
            for (var i = 0; i < unfiltredArray.Count; i++)
                if (unfiltredArray[i] == criteria)
                    indexList.Add(i);
            return indexList;
        }

        // filter int
        public static List<int> IndexFilterList(List<int> unfiltredArray, int criteria)
        {
            var indexList = new List<int>();
            for (var i = 0; i < unfiltredArray.Count; i++)
                if (unfiltredArray[i] == criteria)
                    indexList.Add(i);
            return indexList;
        }

        public static List<int> IndexFilterList(List<char> unfiltredArray, char criteria)
        {
            var indexList = new List<int>();
            for (var i = 0; i < unfiltredArray.Count; i++)
                if (unfiltredArray[i] == criteria)
                    indexList.Add(i);
            return indexList;
        }
        public static List<int> IndexFilterList(List<double> unfiltredArray, double criteria)
        {
            var indexList = new List<int>();
            for (var i = 0; i < unfiltredArray.Count; i++)
                if (Math.Abs(unfiltredArray[i] - criteria) < 0.000001)
                    indexList.Add(i);
            return indexList;
        }
        
        /*
         * TODO Conditions:
         * double/ int ><=
         * 
         */
        
        #endregion

        #region Compare

        /*
         * used for getting indexes of filtered lists, checks item in order, and only moves forward
         *      returns => List<int>
         */

        public static List<int> Compare(string[] unfiltred, string[] filtred)
        {
            var indexList = new List<int>();
            var item = 0;
            for (var i = 0; i < unfiltred.Length; i++)
            {
                if (unfiltred[i] != filtred[item]) continue;
                indexList.Add(i);
                item++;
            }

            return indexList;
        }

        public static List<int> Compare(int[] unfiltred, int[] filtred)
        {
            var indexList = new List<int>();
            var item = 0;
            for (var i = 0; i < unfiltred.Length; i++)
            {
                if (unfiltred[i] != filtred[item]) continue;
                indexList.Add(i);
                item++;
            }

            return indexList;
        }

        #endregion
    }
}