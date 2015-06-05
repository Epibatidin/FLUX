using Extension.Tree;
using Extraction.Interfaces;

namespace Extraction.Layer.File
{
    public class TreeByKeyAccessor : ISongByKeyAccessor
    {
        private readonly TreeItem<FileLayerSongDO> _tree;

        public TreeByKeyAccessor(TreeItem<FileLayerSongDO> tree)
        {
            _tree = tree;
        }


        public ISong GetByKey(int key)
        {
            return _tree[key].Value;
        }
    }
}
