using System.Collections.Generic;

namespace DataStructure.Tree
{
    public class TreeComparer
    {
        public static bool Compare<T>(TreeItem<T> x, TreeItem<T> y, IEqualityComparer<T> comparer)
        {
            if (x == null && y == null)
                return true;

            if (!comparer.Equals(x.Value, y.Value))
                return false;

            if (!x.HasChildren || !y.HasChildren) return true;
            if (x.Count != y.Count) return false;

            for (int i = 0; i < x.Count; i++)
            {
                if (!Compare(x[i], y[i], comparer))
                    return false;
            }
            return true;
        }
    }
}
