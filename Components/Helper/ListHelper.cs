using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper
{
    public static class ListHelper
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
            if (item != null)
            {
                if (list == null)
                    return false;
                list.Add(item);
                return true;
            }
            return false;
        }

        public static bool FullCheckedAdd<T>(ref List<T> list, T item)
        {
            if (item != null)
            {
                if (list == null)
                    list = new List<T>();
                list.Add(item);
                return true;
            }
            return false;
        }

    }
}
