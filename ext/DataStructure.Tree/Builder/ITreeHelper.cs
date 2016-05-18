using System;
using System.Collections.Generic;

namespace DataStructure.Tree.Builder
{
    public interface ITreeHelper
    {
        void Add<T>(TreeItem<T> root, IEnumerable<int> path, T item);
        void Add<T>(TreeItem<T> root, IEnumerable<int> path, Action<T> valueConfig);
        TreeItem<T> SelectItemOnPath<T>(TreeItem<T> root, IEnumerable<int> path);
    }
}