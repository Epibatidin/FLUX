using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;
using DataStructure.Tree;
using DataStructure.Tree.Builder;
using Extraction.Interfaces;
using FLUX.DomainObjects;
using FLUX.Interfaces;

namespace FLUX.Web.Logic
{
    public class LayerResultJoiner : ILayerResultJoiner
    {
        private readonly ITreeHelper _treeHelper;

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
        
        public LayerResultJoiner(ITreeHelper treeHelper)
        {
            _treeHelper = treeHelper;
        }

        public TreeItem<MultiLayerDataContainer> Build(IList<IVirtualFile> virtualFiles, 
            IList<ISongByKeyAccessor> songByKeyAccessors)
        {
            var root = new TreeItem<MultiLayerDataContainer>();

            var flatListForSongs = new List<object>();

            foreach (var kv in virtualFiles)
            {
                // the path is relevant 
                //var path = IntToDigits(kv.ID);

                //var activeTreeItem = _treeHelper.SelectItemOnPath(root, kv.ID);



                var container = new MultiLayerDataContainer();
                container.Id = kv.ID;
                container.Path = kv.Name;

                foreach (var byKeyAccessor in songByKeyAccessors)
                {
                    var song = byKeyAccessor.GetByKey(kv.ID);

                    container.AddSong(song);
                }
            }

            //var childs = new List<TreeItem<MultiLayerDataContainer>>();
            //LayerData.SetChildren(childs);

            //foreach (var virtualFile in virtualFiles.Values)
            //{
            //    var childData = new MultiLayerDataContainer();
            //    childData.AddSong(1, new DummySong()
            //    {
            //        Id = virtualFile.ID,
            //        Album = virtualFile.VirtualPath
            //    });
            //    var childTree = new TreeItem<MultiLayerDataContainer>();
            //    childTree.Value = childData;
            //    childs.Add(childTree);
            //}


            return null;
        }
    }
}
