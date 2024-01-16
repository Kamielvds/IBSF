using System.Collections.Generic;
using System.Linq;
using static Filtering.Filters;

namespace Commands
{
    public static class FilterCmd
    {
        public static List<int> Filtred;

        /*
         * Update the Filtered Property
         */
        
        #region UpdateFilter

        public static void UpdateFilter(List<char> list, char criteria)
        {
            Filtred = IndexFilterList(list, criteria);
        }
        public static void UpdateFilter(List<byte> list, byte criteria)
        {
            Filtred = IndexFilterList(list, criteria);
        }
        public static void UpdateFilter(List<double> list, double criteria)
        {
            Filtred = IndexFilterList(list, criteria);
        }
        public static void UpdateFilter(List<int> list, int criteria)
        {
            Filtred = IndexFilterList(list, criteria);
        }
        public static void UpdateFilter(List<string> list, string criteria)
        {
            Filtred = IndexFilterList(list, criteria);
        }

        #endregion


        /*
         * Return Value's of filtered list based on Filtered List
         */
        #region FiltredList
        
        private static List<string> FilteredList(IReadOnlyList<string> list)
        {
            if (Filtred == null) return new List<string>();
            var result = Filtred.Select(i => list[i]).ToList();

            return result;
        }
        private static List<int> FilteredList(IReadOnlyList<int> list)
        {
            if (Filtred == null) return new List<int>();
            var result =  Filtred.Select(i => list[i]).ToList();

            return result;
        }
        private static List<char> FilteredList(IReadOnlyList<char> list)
        {
            if (Filtred == null) return new List<char>();
            var result =  Filtred.Select(i => list[i]).ToList();

            return result;
        }
        private static List<double> FilteredList(IReadOnlyList<double> list)
        {
            if (Filtred == null) return new List<double>();
            var result =  Filtred.Select(i => list[i]).ToList();

            return result;
        }
        private static List<byte> FilteredList(IReadOnlyList<byte> list)
        {
            if (Filtred == null) return new List<byte>();
            var result =  Filtred.Select(i => list[i]).ToList();

            return result;
        }

        #endregion        
        
    }
}