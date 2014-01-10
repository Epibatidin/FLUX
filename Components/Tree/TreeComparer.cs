using System.Collections.Generic;

namespace Tree
{
    public class TreeComparer
    {
        public static bool Compare<T>(TreeItem<T> X, TreeItem<T> Y, IEqualityComparer<T> _comparer)
        {
            if (X == null && Y == null)
                return true;

            if (!_comparer.Equals(X.Value, Y.Value))
                return false;

            if (X.HasChildren && Y.HasChildren)
            {
                if (X.Count != Y.Count)
                    return false;
                else
                {
                    for (int i = 0; i < X.Count; i++)
                    {
                        if (!Compare(X[i], Y[i], _comparer))
                            return false;
                    }
                }
            }
            return true;
        }

    }
}
