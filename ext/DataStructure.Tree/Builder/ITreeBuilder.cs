using System;
using System.Collections.Generic;

namespace DataStructure.Tree.Builder
{
    public interface ITreeBuilder
    {
        void Add<T>(TreeItem<T> root, IEnumerable<int> path, T item) where T : class;
        //void Add<T>(TreeItem<T> root, IEnumerable<int> path, Action<T> valueConfig);
        //TreeItem<T> BuildToPath<T>(TreeItem<T> root, IEnumerable<int> path);

        TreeItem<TTreeItemValue> BuildTreeFromCollection<TCollectionItem, TTreeItemValue>(
            IEnumerable<TCollectionItem> collection, Func<TCollectionItem, int, string> keyAccessor,
            Func<TCollectionItem, int, TTreeItemValue> itemValueMapper)
            where TTreeItemValue : class;
    }
}