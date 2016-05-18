using System;
using System.Collections.Generic;

namespace DataStructure.Tree.Builder
{
    public class TreeHelper : ITreeHelper 
    {
        public void Add<T>(TreeItem<T> root, IEnumerable<int> path, T item)
        {
            var movingRoot = SelectItemOnPath(root, path);
            movingRoot.Value = item;
        }

        public TreeItem<T> SelectItemOnPath<T>(TreeItem<T> root, IEnumerable<int> path)
        {
            int currentLvl = root.Level;

            var movingRoot = root;
            foreach (var index in path)
            {
                currentLvl++;
                var childs = movingRoot.GetChildren();
                if (childs == null)
                {
                    childs = new List<TreeItem<T>>();
                    movingRoot.SetChildren(childs);
                }
                if (index >= childs.Count - 1)
                {
                    var itemCountToAdd = index + 1 - childs.Count;

                    for (int i = 0; i < itemCountToAdd; i++)
                    {
                        childs.Add(new TreeItem<T>()
                        {
                            Level = currentLvl
                        });
                    }
                }
                movingRoot = childs[index];
            }
            return movingRoot;
        }

        public void Add<T>(TreeItem<T> root, IEnumerable<int> path, Action<T> valueConfig)
        {
            var movingRoot = SelectItemOnPath(root, path);
            valueConfig(movingRoot.Value);
        }
    }
}
