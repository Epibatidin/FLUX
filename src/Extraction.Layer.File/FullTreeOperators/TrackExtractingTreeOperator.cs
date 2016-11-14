using DataStructure.Tree.Iterate;
using Extraction.Layer.File.FullTreeOperators.InnerOperators;
using System.Linq;

namespace Extraction.Layer.File.Operations.FullTreeOperators
{
    public class TrackExtractingTreeOperator : IFullTreeOperator
    {
        ITrackExtractor _trackExtractor;

        public TrackExtractingTreeOperator(ITrackExtractor trackExtractor)
        {
            _trackExtractor = trackExtractor;
        }

        public void Operate(TreeByKeyAccessor treeAccessor)
        {
            var iterator = new MaxLevelEnumerator<FileLayerSongDo>(treeAccessor.Tree, 2, true);

            while(iterator.MoveNext())
            {
                _trackExtractor.Execute(iterator.Current.Children.Select(c => c.Value).ToList());
            }
        }
    }
}
