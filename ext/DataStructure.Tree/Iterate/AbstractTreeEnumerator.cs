using System.Collections.Generic;

namespace DataStructure.Tree.Iterate
{
    public abstract class AbstractTreeEnumerator<T> : IEnumerator<TreeItem<T>>
    {
        protected AbstractTreeEnumerator(TreeItem<T> root)
        {
            Root = root;
        }


        protected TreeItem<T> Root;

        protected TreeItem<T> current;

        object System.Collections.IEnumerator.Current => Current;
        public TreeItem<T> Current => current;


        public virtual void Dispose()
        {
        }

        public abstract bool MoveNext();

        public abstract void Reset();
    }
}
