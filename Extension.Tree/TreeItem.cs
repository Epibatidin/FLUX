using System.Collections.Generic;
using System.Linq;

namespace Extension.Tree
{
    public interface ITreeItem<out T>
    {
        int Level { get; }

        T Value { get; }

        IEnumerable<ITreeItem<T>> Children { get; }

        bool HasChildren { get; }
    }
    

    public class TreeItem<T> : ITreeItem<T> 
    {
        public int Level { get; set; }

        public T Value { get; set; }

        private List<TreeItem<T>> _childs;

        public IEnumerable<ITreeItem<T>> Children 
        {
            get
            {
                return _childs;
            }
        }

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
                else
                    return _childs.Count;
            }
        }
    }
}
