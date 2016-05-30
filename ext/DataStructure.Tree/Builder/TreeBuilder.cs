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

        public class SavedPathData
        {
            public int NextOnLevel { get; set; }

            public List<int> Path { get; set; }
        }

        public class DictItem
        {
            public DictItem()
            {
                KnownPathes = new List<SavedPathData>();
            }

            public int ActiveIndex { get; set; }

            public List<SavedPathData> KnownPathes { get; set; }

            public SavedPathData ActivePath
            {
                get { return KnownPathes[ActiveIndex]; }
            }
        }

        private bool SetActivePathToKnownOne(DictItem availablePathes, SavedPathData currentPathContainer)
        {
            for (int index = 0; index < availablePathes.KnownPathes.Count; index++)
            {
                var knownPathContainer = availablePathes.KnownPathes[index];
                var knownPath = knownPathContainer.Path;
                var currentPath = currentPathContainer.Path;

                // currentPath will always be one level shorter 
                bool equal = true;
                if (knownPath.Count == currentPath.Count + 1)
                {
                    for (int i = 0; i < currentPath.Count; i++)
                    {
                        if (knownPath[i] != currentPath[i])
                        {
                            equal = false;
                            break;
                        }
                    }
                }
                if (!equal) continue;

                availablePathes.ActiveIndex = index;

                return true;
            }
            return false;
        }


        public TreeItem<TTreeItemValue> BuildTreeFromCollectionForTests<TCollectionItem, TTreeItemValue>(
            IEnumerable<TCollectionItem> collection,
            Func<TCollectionItem, int, string> keyAccessor, Func<TCollectionItem, int, TTreeItemValue> itemValueMapper,
            Dictionary<string, DictItem> keys)
            where TTreeItemValue : class
        {
            // ich muss mir zum pfad merken wieviele items 
            // bereits dort sind 
            // dann kann ich den baum aber auch direkt bauen 

            if (keys == null)
                keys = new Dictionary<string, DictItem>();

            var root = new TreeItem<TTreeItemValue>();

            foreach (var someItem in collection)
            {
                SavedPathData previousMatch = null;

                for (int i = 0; ; i++)
                {
                    var key = keyAccessor(someItem, i);
                    if (key == null) break;

                    if (keys.ContainsKey(key))
                    {
                        var dictItem = keys[key];
                        // select the activePath here ? 
                        if (previousMatch?.Path.Count > 0)
                        {
                            // selectier in der menge der KnownPathes the activePath by current Path
                            // alternativ füge hinzu und setze aktiv 
                            var isKnownPath = SetActivePathToKnownOne(dictItem, previousMatch);
                            if (!isKnownPath)
                            {
                                // this is a new path 
                                var path = new List<int>(previousMatch.Path);
                                path.Add(previousMatch.NextOnLevel++);

                                dictItem.ActiveIndex = dictItem.KnownPathes.Count;
                                dictItem.KnownPathes.Add(new SavedPathData()
                                {
                                    Path = path
                                });
                            }
                        }
                        previousMatch = dictItem.ActivePath;
                    }
                    else
                    {
                        var newWrapper = new DictItem();
                        if (previousMatch != null)
                        {
                            // select matching path 
                            var list = new List<int>(previousMatch.Path);
                            list.Add(previousMatch.NextOnLevel++);

                            newWrapper.ActiveIndex = newWrapper.KnownPathes.Count;
                            newWrapper.KnownPathes.Add(new SavedPathData()
                            {
                                Path = list,
                            });
                        }
                        else
                        {
                            newWrapper.KnownPathes.Add(new SavedPathData() { Path = new List<int>() });
                        }
                        previousMatch = newWrapper.ActivePath;
                        keys.Add(key, newWrapper);
                    }
                }
                if (previousMatch == null)
                    throw new NotSupportedException("TreeItems that do not have a key are not supported");

                BuildToPath(root, previousMatch.Path, (collectionItem, depth, isLastItem) => itemValueMapper(collectionItem, depth), someItem);
            }
            return root;
        }

        public TreeItem<TTreeItemValue> BuildTreeFromCollection<TCollectionItem, TTreeItemValue>(IEnumerable<TCollectionItem> collection,
            Func<TCollectionItem, int, string> keyAccessor, Func<TCollectionItem, int, TTreeItemValue> itemValueMapper)
            where TTreeItemValue : class
        {
            return BuildTreeFromCollectionForTests(collection, keyAccessor, itemValueMapper, null);
        }


        public TreeItem<TTreeItemValue> BuildToPath<TCollectionItem, TTreeItemValue>(TreeItem<TTreeItemValue> root, IEnumerable<int> path,
            Func<TCollectionItem, int, bool, TTreeItemValue> itemValueMapper, TCollectionItem value)
            where TTreeItemValue : class
        {
            int currentLvl = root.Level;

            var movingRoot = root;

            if (movingRoot.Value == null)
                movingRoot.Value = itemValueMapper(value, currentLvl, true);
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
            return movingRoot;
        }

    }
}