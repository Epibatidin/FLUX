using System.Collections.Generic;
using System.Collections;

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

        object IEnumerator.Current => current;
        public TreeItem<T> Current => current;


        public virtual void Dispose()
        {
        }

        public abstract bool MoveNext();

        public abstract void Reset();
    }
}
