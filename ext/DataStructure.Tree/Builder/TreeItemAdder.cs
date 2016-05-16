using System.Collections.Generic;

namespace DataStructure.Tree.Builder
{
    public class TreeItemAdder
    {
        public void Add<T>(TreeItem<T> root, IList<int> path, T item)
        {
            int depth = 0;

            var movingRoot = root;
            foreach (var index in path)
            {
                var childs = movingRoot.GetChildren();
                if (childs == null)
                {
                    childs = new List<TreeItem<T>>();
                    movingRoot.SetChildren(childs);
                }
                if (index >= childs.Count -1)
                {
                    var itemCountToAdd = index + 1 - childs.Count;

                    for (int i = 0; i < itemCountToAdd; i++)
                    {
                        childs.Add(new TreeItem<T>());
                    }
                }
                movingRoot = childs[index];
                movingRoot.Value = item;
            }

           
        }

    }
}
