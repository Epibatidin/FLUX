using System.Collections.Generic;

namespace Common.Tree
{
    public class TreeItem<T> 
    {
        public T Value { get; set; }

        public IEnumerable<TreeItem<T>> Children { get; set; }

    }
}
