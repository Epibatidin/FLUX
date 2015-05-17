using AbstractDataExtraction;
using Interfaces;
using Tree;

namespace FileStructureDataExtraction
{
    public class TreeByKeyAccessor : ISongByKeyAccessor
    {
        private readonly TreeItem<FileLayerSongDO> _tree;

        public TreeByKeyAccessor(Tree.TreeItem<FileStructureDataExtraction.FileLayerSongDO> tree)
        {
            _tree = tree;
        }


        public ISong GetByKey(int key)
        {
            return _tree[key].Value;
        }
    }
}
