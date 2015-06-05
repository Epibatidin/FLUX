using System.Collections.Generic;

namespace Extension.Tree
{
    public class TreeComparer
    {
        public static bool Compare<T>(TreeItem<T> X, TreeItem<T> Y, IEqualityComparer<T> comparer)
        {
            if (X == null && Y == null)
                return true;

            if (!comparer.Equals(X.Value, Y.Value))
                return false;

            if (!X.HasChildren || !Y.HasChildren) return true;
            if (X.Count != Y.Count) return false;

            for (int i = 0; i < X.Count; i++)
            {
                if (!Compare(X[i], Y[i], comparer))
                    return false;
            }
            return true;
        }
    }
}
