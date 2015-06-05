using System.Collections.Generic;

namespace Extension.Tree.Iterate
{
    public abstract class AbstractTreeEnumerator<T> : IEnumerator<TreeItem<T>>
    {
        internal AbstractTreeEnumerator(TreeItem<T> root)
        {
            Root = root;
        }


        protected TreeItem<T> Root;

        object System.Collections.IEnumerator.Current { get { return Current; } }
        protected TreeItem<T> current;
        public TreeItem<T> Current
        {
            get
            {
                return current;
            }
        }


        public virtual void Dispose()
        {
        }

        public abstract bool MoveNext();

        public abstract void Reset();
    }
}
