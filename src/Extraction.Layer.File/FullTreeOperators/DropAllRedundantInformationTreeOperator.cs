using DataStructure.Tree;
using DataStructure.Tree.Iterate;
using Extraction.Layer.File.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Extraction.Layer.File.FullTreeOperators
{
    public class DropAllRedundantInformationTreeOperator : IFullTreeOperator
    {
        private IDropInformationInAllElementsOnThisLvlOperation _innerOperator;

        public DropAllRedundantInformationTreeOperator(IDropInformationInAllElementsOnThisLvlOperation innerOperator)
        {
            _innerOperator = innerOperator;
        }

        private IEnumerable<IEnumerable<ITreeItem<FileLayerSongDo>>> 
            IterateInBlocks(TreeItem<FileLayerSongDo> root)
        {
            yield return root.Children; // Alben

            var albumIterator = new MaxLevelEnumerator<FileLayerSongDo>(root, 1, true);
            while (albumIterator.MoveNext())
            {
                yield return albumIterator.Current.Children; // CDs
            }

            var songIterator = new MaxLevelEnumerator<FileLayerSongDo>(root, 2, true);
            while (songIterator.MoveNext())
            {
                yield return songIterator.Current.Children; // Songs
            }
        }

        public void Operate(TreeByKeyAccessor treeByKeyAccessor)
        {
            foreach(var block in IterateInBlocks(treeByKeyAccessor.Tree))
            {
                var list = block.Select(c => c.Value.LevelValue).ToList();

                _innerOperator.Operate(list);
            }
        }
    }
}
