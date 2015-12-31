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

        public ISong GetByKey(int key)
        {
            return _tree[key].Value;
        }
    }
}
