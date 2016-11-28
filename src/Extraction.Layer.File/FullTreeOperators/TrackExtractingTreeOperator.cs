using DataStructure.Tree;
using DataStructure.Tree.Iterate;
using Extraction.Layer.File.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Extraction.Layer.File.FullTreeOperators
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
                var current = iterator.Current;

                var tracks = current.Children.Select(c => c.Value).ToList();
                
                // we dont expcet das multiple cds have tracks that would lead to even more cds 
                if (!_trackExtractor.Execute(tracks, int.Parse(current.Value.CD))) continue;

                var albumNode = PathEnumerator<FileLayerSongDo>.NavigateToItem(treeAccessor.Tree, iterator.ActivePath(), 1);

                var listOfCds = albumNode.GetChildren();

                var trackNodes = current.GetChildren();
                for (int i = 0; i < trackNodes.Count; i++)
                {
                    var track = trackNodes[i];                   
                    if (track.Value.CDRaw == 1) continue;
                    
                    AssureLengthOfCds(listOfCds, track.Value.CDRaw);
                    trackNodes.RemoveAt(i--);

                    listOfCds[track.Value.CDRaw - 1].Add(track);
                }

                for (int cdCount = 0; cdCount < listOfCds.Count; cdCount++)
                {
                    var cd = listOfCds[cdCount];
                    cd.Value.SetByDepth(2, (cdCount + 1).ToString());

                    for (int trackCount = 0; trackCount < listOfCds[cdCount].Count; trackCount++)
                    {
                        var track = cd[trackCount].Value;

                        var path = treeAccessor.KeyMappings[track.Id];

                        path[2] = trackCount;
                        path[1] = cdCount;
                    }
                }
            }
        }

        private void AssureLengthOfCds(List<TreeItem<FileLayerSongDo>> cds, int cd)
        {
            // cds is length 1 
            // cd is 1 - valid

            // cds is length 1 
            // cd is 3 - add 3 items
            int begin = cds.Count;
            for (int i = begin; i < cd; i++)
            {
                var fileLayerSong = new FileLayerSongDo();
                
                cds.Add(new TreeItem<FileLayerSongDo>()
                {
                    Level = 2,
                    Value = fileLayerSong
                });
            }            
        }

        private void MoveToNewItem() { }
    }
}
