using System.Collections.Generic;
using System.Linq;
using DataStructure.Tree;
using Extraction.Interfaces;

namespace Extraction.Layer.File
{
    public class TreeByKeyAccessor : ISongByKeyAccessor
    {
        private readonly TreeItem<FileLayerSongDo> _tree;

        public TreeByKeyAccessor(TreeItem<FileLayerSongDo> tree)
        {
            _tree = tree;
        }

        private IEnumerable<int> IntToDigits(int key)
        {
            return key.ToString().Select(c => c - 48);
        }

        public ISong GetByKey(int key)
        {
            var root = _tree;

            foreach (var digit in IntToDigits(key))
            {
                root = root[digit];
            }
            return root.Value;
        }
    }
}
