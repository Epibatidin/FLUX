using System;
using System.Collections.Generic;

namespace DataStructure.Tree.Builder
{
    public class TreeBuilder : ITreeBuilder
    {
        public void Add<T>(TreeItem<T> root, IEnumerable<int> path, T item) where T : class
        {
            var movingRoot = BuildToPath(root, path, (c, i, t) => null, item);
            movingRoot.Value = item;
        }

    
        private TreeItem<TTreeItemValue> BuildToPath<TCollectionItem,TTreeItemValue>(TreeItem<TTreeItemValue> root, IEnumerable<int> path,
            Func<TCollectionItem, int, bool, TTreeItemValue> itemValueMapper, TCollectionItem value)
            where TTreeItemValue : class 

        {
            int currentLvl = root.Level;

            var movingRoot = root;
            foreach (var index in path)
            {
                currentLvl++;
                var childs = movingRoot.GetChildren();
                if (childs == null)
                {
                    childs = new List<TreeItem<TTreeItemValue>>();
                    movingRoot.SetChildren(childs);
                }
                if (index >= childs.Count - 1)
                {
                    var itemCountToAdd = index + 1 - childs.Count;

                    for (int i = 0; i < itemCountToAdd; i++)
                    {
                        childs.Add(new TreeItem<TTreeItemValue>()
                        {
                            Level = currentLvl
                        });
                    }
                }
                if (childs[index].Value == null)
                    childs[index].Value = itemValueMapper(value, currentLvl, false);
                movingRoot = childs[index];
            }

            if (movingRoot.Value == null)
                movingRoot.Value = itemValueMapper(value, currentLvl, true);

            return movingRoot;
        }

        //public void Add<T>(TreeItem<T> root, IEnumerable<int> path, Action<T> valueConfig)
        //{
        //    var movingRoot = BuildToPath(root, path);
        //    valueConfig(movingRoot.Value);
        //}

        //public void Add<TCollectionItem, TTreeItemValue>(TreeItem<TTreeItemValue> root, IEnumerable<int> path, TCollectionItem collectionItem,
        //    Func<TCollectionItem, int, TTreeItemValue> itemValueMapper)
        //{
        //    var movingRoot = BuildToPath(root, path);
        //    valueConfig(movingRoot.Value);
        //}

        private class SavedPathData
        {
            public int NextOnLevel { get; set; }

            public List<int> Path { get; set; }
        }

        public TreeItem<TTreeItemValue> BuildTreeFromCollection<TCollectionItem, TTreeItemValue>(IList<TCollectionItem> collection,
            Func<TCollectionItem, int, string> keyAccessor, Func<TCollectionItem, int, TTreeItemValue> itemValueMapper)

            where TTreeItemValue : class 

        {
            var root = new TreeItem<TTreeItemValue>();

            var keys = new Dictionary<string, SavedPathData>();

            // ich muss mir zum pfad merken wieviele items 
            // bereits dort sind 
            // dann kann ich den baum aber auch direkt bauen 
            foreach (var someItem in collection)
            {
                SavedPathData previousMatch = null;

                for (int i = 0;; i++)
                {
                    var key = keyAccessor(someItem, i);
                    if (key == null) break;

                    if (keys.ContainsKey(key))
                    {
                        previousMatch = keys[key];
                    }
                    else
                    {
                        var newWrapper = new SavedPathData();
                        if (previousMatch != null)
                        {
                            newWrapper.Path = new List<int>(previousMatch.Path);
                            newWrapper.Path.Add(previousMatch.NextOnLevel++);
                        }
                        else
                        {
                            newWrapper.Path = new List<int> {0};
                        }
                        previousMatch = newWrapper;
                        keys.Add(key, newWrapper);
                    }
                }
                if (previousMatch == null)
                    throw new NotSupportedException("TreeItems that do not have a key are not supported");
                
                BuildToPath(root, previousMatch.Path, (collectionItem, depth, isLastItem) => itemValueMapper(collectionItem, depth), someItem);
            }
            return root;
        }

    }
}