using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;
using DataStructure.Tree;
using DataStructure.Tree.Builder;
using Extraction.Interfaces;
using FLUX.DomainObjects;
using FLUX.Interfaces;
using JoinerTreeItem = DataStructure.Tree.TreeItem<FLUX.DomainObjects.MultiLayerDataContainer>;

namespace FLUX.Web.Logic
{
    public class LayerResultJoiner : ILayerResultJoiner
    {
        private readonly ITreeBuilder _treeBuilder;
        
        public LayerResultJoiner(ITreeBuilder treeBuilder)
        {
            _treeBuilder = treeBuilder;
        }
        
        public JoinerTreeItem Build(IList<IVirtualFile> virtualFiles, 
            IList<ISongByKeyAccessor> songByKeyAccessors)
        {
            var flatListOfSongs = BuildInitalFlatMultiLayerCollection(virtualFiles, songByKeyAccessors);
            
            var tree = _treeBuilder.BuildTreeFromCollection(flatListOfSongs,
                (container, i) => container.GetGroupingKeyByDepth(i), (a, b) => a);

            return tree[0];
        }

        public IList<MultiLayerDataContainer> BuildInitalFlatMultiLayerCollection(IEnumerable<IVirtualFile> virtualFiles,
            IList<ISongByKeyAccessor> songByKeyAccessors)
        {
            var flatListOfSongs = new List<MultiLayerDataContainer>();
            foreach (var kv in virtualFiles)
            {
                var container = new MultiLayerDataContainer();
                container.Id = kv.ID;
               
                flatListOfSongs.Add(container);
                foreach (var byKeyAccessor in songByKeyAccessors)
                {
                    var song = byKeyAccessor.GetByKey(kv.ID);

                    container.AddSong(song);
                }
            }
            return flatListOfSongs;
        }
    }
}
