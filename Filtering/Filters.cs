﻿using System;
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

        public static List<string> FilterList(List<string> unfilteredArray, string criteria)
        {
            return unfilteredArray.Where(str => str == criteria).ToList();
        }

        public static List<int> FilterList(List<int> unfilteredArray, int criteria)
        {
            return unfilteredArray.Where(i => i == criteria).ToList();
        }

        public static List<double> FilterList(List<double> unfilteredArray, int criteria)
        {
            return unfilteredArray.Where(i => Math.Abs(i - criteria) < 0.00001)
                .ToList(); // using tolerance of 5 digits  
        }
        public static List<char> FilterList(List<char> unfilteredArray, char criteria)
        {
            return unfilteredArray.Where(i => i == criteria).ToList();
        }

        #endregion

        #region registerd

        /*
         * used for getting the address of a filtred list
         *      returns => List<int>
         */

        // filter string
        public static List<int> IndexFilterList(List<string> unfilteredArray, string criteria)
        {
            var indexList = new List<int>();
            for (var i = 0; i < unfilteredArray.Count; i++)
                if (unfilteredArray[i] == criteria)
                    indexList.Add(i);
            return indexList;
        }

        // filter int
        public static List<int> IndexFilterList(List<int> unfilteredArray, int criteria)
        {
            var indexList = new List<int>();
            for (var i = 0; i < unfilteredArray.Count; i++)
                if (unfilteredArray[i] == criteria)
                    indexList.Add(i);
            return indexList;
        }

        public static List<int> IndexFilterList(List<char> unfilteredArray, char criteria)
        {
            var indexList = new List<int>();
            for (var i = 0; i < unfilteredArray.Count; i++)
                if (unfilteredArray[i] == criteria)
                    indexList.Add(i);
            return indexList;
        }
        public static List<int> IndexFilterList(List<double> unfilteredArray, double criteria)
        {
            var indexList = new List<int>();
            for (var i = 0; i < unfilteredArray.Count; i++)
                if (Math.Abs(unfilteredArray[i] - criteria) < 0.000001)
                    indexList.Add(i);
            return indexList;
        }
        public static List<int> IndexFilterList(List<byte> unfilteredArray, byte criteria)
        {
            var indexList = new List<int>();
            for (var i = 0; i < unfilteredArray.Count; i++)
                if (unfilteredArray[i] == criteria)
                    indexList.Add(i);
            return indexList;
        }
        
        /*
         * TODO Conditions:
         * double/ int <>
         * 
         */
        
        #endregion

        #region Compare

        /*
         * used for getting indexes of filtered lists, checks item in order, and only moves forward
         *      returns => List<int>
         */

        public static List<int> Compare(string[] unfiltered, string[] filtered)
        {
            var indexList = new List<int>();
            var item = 0;
            for (var i = 0; i < unfiltered.Length; i++)
            {
                if (unfiltered[i] != filtered[item]) continue;
                indexList.Add(i);
                item++;
            }

            return indexList;
        }

        public static List<int> Compare(int[] unfiltered, int[] filtered)
        {
            var indexList = new List<int>();
            var item = 0;
            for (var i = 0; i < unfiltered.Length; i++)
            {
                if (unfiltered[i] != filtered[item]) continue;
                indexList.Add(i);
                item++;
            }

            return indexList;
        }

        #endregion

        #region UniqueValues

        public static List<string> UniqueValues(IEnumerable<string> list)
        {
            var result = new List<string>();
            foreach (var value in list.Where(value => result.Contains(value)))
                result.Add(value);
            return result;
        }
        public static List<int> UniqueValues(IEnumerable<int> list)
        {
            var result = new List<int>();
            foreach (var value in list.Where(value => result.Contains(value)))
                result.Add(value);
            return result;
        }
        public static List<double> UniqueValues(IEnumerable<double> list)
        {
            var result = new List<double>();
            foreach (var value in list.Where(value => result.Contains(value)))
                result.Add(value);
            return result;
        }
        public static List<byte> UniqueValues(IEnumerable<byte> list)
        {
            var result = new List<byte>();
            foreach (var value in list.Where(value => result.Contains(value)))
                result.Add(value);
            return result;
        }
        public static List<char> UniqueValues(IEnumerable<char> list)
        {
            var result = new List<char>();
            foreach (var value in list.Where(value => result.Contains(value)))
                result.Add(value);
            return result;
        }

        #endregion
        
    }
}