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
            //var tree = TraverseListToTree(flatListOfSongs);

            var tree = _treeBuilder.BuildTreeFromCollection(flatListOfSongs,
                (container, i) => container.GetGroupingKeyByDepth(i),
                (a, b) => a);

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
                container.Path = kv.VirtualPath;
                flatListOfSongs.Add(container);
                foreach (var byKeyAccessor in songByKeyAccessors)
                {
                    var song = byKeyAccessor.GetByKey(kv.ID);

                    container.AddSong(song);
                }
            }
            return flatListOfSongs;
        }

        //public JoinerTreeItem TraverseListToTree(IList<MultiLayerDataContainer> flatList)
        //{
        //    var root = RecursiveWtf(flatList, 0);

        //    var newRoot = new TreeItem<MultiLayerDataContainer>();
        //    newRoot.SetChildren(root);
        //    return newRoot;
        //}
        
        //private List<JoinerTreeItem> RecursiveWtf(IEnumerable<MultiLayerDataContainer> dontKnow ,int depth)
        //{
        //    var filteredEnumerable = dontKnow.Select(c => new {key = c.GetGroupingKeyByDepth(depth), value = c})
        //        .Where(c => c.key != null);
        //    var groupBy = filteredEnumerable.GroupBy(c => c.key, c => c.value, new MyEqualityComparer());

        //    List<JoinerTreeItem> treeNodes = null;

        //    bool isFirstItem = true;
        //    foreach (var group in groupBy)
        //    {
        //        var child = new JoinerTreeItem();
        //        child.Level = depth;
                
        //        if (isFirstItem)
        //        {
        //            treeNodes = new List<JoinerTreeItem>();
        //            isFirstItem = false;
        //        }
        //        var current = group.First();
        //        child.Value = current;
        //        //foreach (var multiLayerDataContainer in group)
        //        //{
        //        //    var treeNode = new TreeItem<MultiLayerDataContainer>();
        //        //    treeNode.Value = multiLayerDataContainer;
        //        //}
        //        var childs = RecursiveWtf(group, depth +1);
        //        if (childs != null)
        //            child.SetChildren(childs);

        //        treeNodes.Add(child);
        //    }
        //    return treeNodes;
        //}

        public class MyEqualityComparer : IEqualityComparer<IList<string>>
        {
            public bool Equals(IList<string> x, IList<string> y)
            {
                if (x == null && y == null) return true;
                if (x?.Count != y?.Count) return false;

                for (int i = 0; i < y.Count; i++)
                {
                    if (x[i] != y[i]) return false;
                }
                return true;
            }

            public int GetHashCode(IList<string> obj)
            {
                return string.Join(",", obj).GetHashCode();
            }
        }


        //private List<TreeItem<FileLayerSongDo>> BuildTree(IEnumerable<Tuple<int, List<string>>> data, int depth)
        //{
        //    // wenn depth == items2.length 
        //    // break;

        //    var grped = from r in
        //                    (from item in data
        //                     where item.Item2.Count > depth
        //                     select item)
        //                group r by r.Item2[depth] into grp
        //                select grp;

        //    List<TreeItem<FileLayerSongDo>> result = null;
        //    bool hasElements = false;

        //    foreach (var item in grped)
        //    {
        //        if (!hasElements)
        //        {
        //            result = new List<TreeItem<FileLayerSongDo>>();
        //            hasElements = true;
        //        }
        //        var child = new TreeItem<FileLayerSongDo>();
        //        child.Level = depth;
        //        var temp = new FileLayerSongDo();
        //        temp.SetByDepth(depth, item.Key);
        //        var current = item.First();

        //        if (depth == current.Item2.Count - 1)
        //            temp.Id = item.First().Item1;

        //        child.Value = temp;

        //        var childs = BuildTree(item, depth + 1);
        //        if (childs != null)
        //            child.SetChildren(childs);

        //        result.Add(child);
        //    }
        //    return result;
        //}
    }
}
