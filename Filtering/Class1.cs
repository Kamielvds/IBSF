using System;
using System.Collections.Generic;
using System.Linq;

namespace Filtering
{
    public class Class1
    {
        #region unregistred

        /*
         * used for filtering a List, without indexes
         *      returns => List<t_var>
         */

        public static List<string> FilterList(IEnumerable<string> unfiltredArray, string criteria)
        {
            return unfiltredArray.Where(str => str == criteria).ToList();
        }

        public static List<int> FilterList(IEnumerable<int> unfiltredArray, int criteria)
        {
            return unfiltredArray.Where(i => i == criteria).ToList();
        }

        public static List<double> FilterList(IEnumerable<double> unfiltredArray, int criteria)
        {
            return unfiltredArray.Where(i => Math.Abs(i - criteria) < 0.00001)
                .ToList(); // using tolerance of 5 digits  
        }

        #endregion

        #region registerd

        /*
         * used for getting the address of a filtred list
         *      returns => List<int>
         */

        // filter string
        public static List<int> IndexFilterList(string[] unfiltredArray, string criteria)
        {
            var indexList = new List<int>();
            for (var i = 0; i < unfiltredArray.Length; i++)
                if (unfiltredArray[i] == criteria)
                    indexList.Add(i);
            return indexList;
        }

        // filter int
        public static List<int> IndexFilterList(int[] unfiltredArray, int criteria)
        {
            var indexList = new List<int>();
            for (var i = 0; i < unfiltredArray.Length; i++)
                if (unfiltredArray[i] == criteria)
                    indexList.Add(i);
            return indexList;
        }

        #endregion

        #region Compare

        /*
         * used for getting indexes of filtred lists, checks item in order, and only moves forward
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