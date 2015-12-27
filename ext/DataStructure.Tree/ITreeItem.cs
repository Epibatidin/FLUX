using System.Collections.Generic;

namespace DataStructure.Tree
{
    public interface ITreeItem<out T>
    {
        int Level { get; }

        T Value { get; }

        IEnumerable<ITreeItem<T>> Children { get; }

        bool HasChildren { get; }
    }
}