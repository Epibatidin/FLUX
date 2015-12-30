using System.Collections.Generic;
using DataAccess.Interfaces;
using DataStructure.Tree;
using Extraction.Interfaces;
using FLUX.DomainObjects;

namespace FLUX.Web.Logic
{
    public class LayerResultJoiner
    {
        public TreeItem<MultiLayerDataContainer> LayerData;

        public class DummySong : ISong
        {
            public int Id { get; set; }
            public int CD { get; }
            public int TrackNr { get; }
            public int Year { get; }
            public string Artist { get; set; }
            public string Album { get; set; }
            public string SongName { get; }
        }

        public LayerResultJoiner(IDictionary<int, IVirtualFile> virtualFiles)
        {
            BuildTreeFromDict(virtualFiles);
        }

        private void BuildTreeFromDict(IDictionary<int, IVirtualFile> virtualFiles)
        {
            LayerData = new TreeItem<MultiLayerDataContainer>();
            var childs = new List<TreeItem<MultiLayerDataContainer>>();
            LayerData.SetChildren(childs);

            foreach (var virtualFile in virtualFiles.Values)
            {
                var childData = new MultiLayerDataContainer();
                childData.AddSong(1, new DummySong()
                {
                    Id = virtualFile.ID,
                    Album = virtualFile.VirtualPath
                });
                var childTree = new TreeItem<MultiLayerDataContainer>();
                childTree.Value = childData;
                childs.Add(childTree);
            }
        }
    }
}
