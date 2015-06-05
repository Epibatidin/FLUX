using System.Collections.Generic;

namespace Extension.IEnumerable
{
    public static class StaticList
    {
        /// <summary>
        /// if list is null => creates new List 
        /// if item is null => doesnt add
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <returns>true if item could be added</returns>
        public static bool PartialCheckedAdd<T>(this List<T> list, T item)
        {
            if (item == null) return false;

            if (list == null)
                return false;
            list.Add(item);
            return true;
        }

        public static bool FullCheckedAdd<T>(ref List<T> list, T item)
        {
            if (item == null) return false;

            if (list == null)
                list = new List<T>();
            list.Add(item);
            return true;
        }
    }
}
