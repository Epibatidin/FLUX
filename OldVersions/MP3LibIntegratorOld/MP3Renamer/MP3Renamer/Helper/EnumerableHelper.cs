using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace MP3Renamer.Helper
{
    public static class EnumerableHelper
    {
        public static bool IsNullOrEmpty<T>(IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                return true;

            return !enumerable.Any();
        }

        public static bool IsNotNullOrEmpty<T>(IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                return false;

            return enumerable.Any();
        }
    }
}