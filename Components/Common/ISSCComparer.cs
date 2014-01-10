using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.ISSC;

namespace Common
{
    public class ISSCComparer : IComparer<InformationStorageSetContainer> , IEqualityComparer<InformationStorageSetContainer>
    {
        public int Compare(InformationStorageSetContainer x, InformationStorageSetContainer y)
        {
            return 0;
            return -1;
            // compare every value ? 
        }


        /// <summary>
        /// Equals also wirkich absolut gleich - aller werte sind gleich 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>

        public bool Equals(InformationStorageSetContainer x, InformationStorageSetContainer y)
        {
            if (!CheckAreNotNull(x, y))
                return false;
            return EqualsISS(x.FileIIS, y.FileIIS);
        }

        private bool EqualsISS(InformationStorageSet x, InformationStorageSet y)
        {
            if (CheckAreNotNull(x, y))
            {
                if (x.GetKeys().Count() == y.GetKeys().Count())
                {
                    foreach (var key in x.GetKeys())
                    {
                        if (y.getByKey<object>(key) != x.getByKey<object>(key))
                            return false;
                    }
                    return true;
                }
            }
            return false;
        }

        private bool CheckAreNotNull(Object x, Object y)
        {
            if (x == null) return false;
            if (y == null) return false;

            return true;
        }

        public int GetHashCode(InformationStorageSetContainer obj)
        {
            return obj.GetHashCode();
        }
    }
}
