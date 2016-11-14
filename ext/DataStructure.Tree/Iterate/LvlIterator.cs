using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructure.Tree.Iterate
{
    public class LvlIterator<TValue> : IEnumerator<IList<TValue>>
    {
        public LvlIterator(ITreeItem<TValue> root)
        {

        }
        protected IList<TValue> current;
        object IEnumerator.Current => current;
        public IList<TValue> Current => current;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
