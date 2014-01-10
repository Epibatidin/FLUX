using System.Collections.Generic;
using System.Linq;

namespace Tree
{  
    public class EasyTreeItem<T>
    {
        public int Level { get; set; }

        public T Value { get; set; }

        public List<EasyTreeItem<T>> Children { get; set; }

        public bool HasChildren
        {
            get
            {
                if (Children == null) return false;
                return Children.Any();
            }
        }

        public bool Add(EasyTreeItem<T> item)
        {
            if (item != null)
            {
                if (Children == null)
                    Children = new List<EasyTreeItem<T>>();
                Children.Add(item);
                return true;
            }
            return false;
        }
                
        public int Count
        {
            get
            {
                if (Children == null)
                    return 0;
                else
                    return Children.Count;
            }
        }
    }
}
