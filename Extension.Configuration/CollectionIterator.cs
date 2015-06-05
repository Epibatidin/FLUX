using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace Extension.Configuration
{
    public class CollectionIterator<T> : IEnumerator<T> where T : ConfigurationElement , IKeyedElement, new()
    {
        private GenericElementCollection<T> _col;

        public CollectionIterator(GenericElementCollection<T> col)
        {
            _col = col;
        }

        private int pos = -1;
        public bool MoveNext()
        {
            if (++pos < _col.Count)
                _current = _col.Item(pos);

            return pos < _col.Count;
        }

        private T _current;
        object IEnumerator.Current
        {
            get
            {
                return _current;
            }
        }

        public T Current
        {
            get
            {
                return _current;
            }
        }

        public void Reset()
        {
            pos = -1;
        }

        public void Dispose()
        {

        }
    }
}
