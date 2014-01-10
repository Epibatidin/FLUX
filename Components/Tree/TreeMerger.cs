using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces;

namespace Tree
{
    public class TreeMerger
    {
        public static TreeItem<T> Merge<T>(TreeItem<T> A, TreeItem<T> B, IComparer<T> Comparer) where T : IUpdateable
        {
            if (A == null)
                return B;
            if (B == null)
                return A;

            if (A.Level == B.Level)
            {
                if (Comparer.Compare(A.Value, B.Value) == 0)
                {
                    // update 
                    A.Value.Update(B.Value);
                    // handle childs
                    List<TreeItem<T>> Children = new List<TreeItem<T>>();
                    if (A.HasChildren && B.HasChildren)
                    {
                        // durch iterieren und fertig 
                        // beide ham kinder 
                        // ich muss also richtig arbeiten 
                        for (int a = 0; a < A.Count; a++)
                        {
                            for (int b = 0; b < B.Count; b++)
                            {
                                var result = Merge(A[a], B[b], Comparer);
                                if (result != null)
                                {
                                    Children.Add(result);
                                    B.RemoveAt(b);
                                    A.RemoveAt(a);
                                    a--;
                                    break;
                                }
                            }
                        }
                    }

                    if (A.HasChildren)
                        Children.AddRange(A.GetChildren());

                    if (B.HasChildren)
                        Children.AddRange(B.GetChildren());

                    if (Children.Count == 0)
                        Children = null;

                    A.SetChildren(Children);
                }
                else
                    return null;
            }
            return A;
        }

        private static bool compare<T>(IUnique<T> x, IUnique<T> y)
        {
            return x.ID.Equals(y.ID);
        }

    }
}
