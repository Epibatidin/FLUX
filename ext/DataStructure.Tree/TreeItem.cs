using System.Collections.Generic;
using System.Linq;

namespace DataStructure.Tree
{
    public class TreeItem<T> : ITreeItem<T> 
    {
        public int Level { get; set; }

        public T Value { get; set; }

        private List<TreeItem<T>> _childs;

        public IEnumerable<ITreeItem<T>> Children => _childs;

        public void SetChildren(List<TreeItem<T>> childs)
        {
            _childs = childs;
        }


        public List<TreeItem<T>> GetChildren()
        {
            return _childs;
        }
            
        public bool HasChildren
        {
            get
            {
                if (_childs == null) return false;
                return _childs.Any();
            }
        }

        public TreeItem<T> this[int pos]
        {
            get
            {
                return _childs[pos];
            }
            set
            {
                _childs[pos] = value;
            }
        }

        public void RemoveAt(int pos)
        {
            _childs.RemoveAt(pos);
        }

        public bool Add(TreeItem<T> item)
        {
            if (item != null)
            {
                if (_childs == null)
                    _childs = new List<TreeItem<T>>();
                _childs.Add(item);
                return true;
            }
            return false;
        }
                
        public int Count
        {
            get
            {
                if (_childs == null)
                    return 0;

                return _childs.Count;
            }
        }
    }
}
