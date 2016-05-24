using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;
using DataStructure.Tree;
using DataStructure.Tree.Iterate;

namespace Extraction.Layer.File
{
    public class TreeByKeyAccessorBuilder : ITreeByKeyAccessorBuilder
    {
        private List<Tuple<int, List<string>>> PrepareData(IEnumerable<IVirtualFile> data)
        {
            // an dieser stelle empfehle ich den baum auf eine constante größe zu bluben 
            // dh wenn parts.length < 3 
            // add CD1 
            var z = from item in data
                    select Tuple.Create(item.ID, ConvertFileNameToTreeableData(item));

            return z.ToList();
        }

        private List<string> ConvertFileNameToTreeableData(IVirtualFile fi)
        {
            List<string> result = fi.VirtualPath.Split('\\').ToList();
            if (result.Count < 3)
                result.Add("CD1");

            result.Add(fi.Name);
            return result;
        }


        private List<TreeItem<FileLayerSongDo>> BuildTree(IEnumerable<Tuple<int, List<string>>> data, int depth)
        {
            // wenn depth == items2.length 
            // break;

            var grped = from r in
                            (from item in data
                             where item.Item2.Count > depth
                             select item)
                        group r by r.Item2[depth] into grp
                        select grp;

            List<TreeItem<FileLayerSongDo>> result = null;
            bool hasElements = false;

            foreach (var item in grped)
            {
                if (!hasElements)
                {
                    result = new List<TreeItem<FileLayerSongDo>>();
                    hasElements = true;
                }
                var child = new TreeItem<FileLayerSongDo>();
                child.Level = depth;
                var temp = new FileLayerSongDo();
                temp.SetByDepth(depth, item.Key);
                var current = item.First();

                if (depth == current.Item2.Count - 1)
                    temp.Id = current.Item1;

                child.Value = temp;

                var childs = BuildTree(item, depth + 1);
                if (childs != null)
                    child.SetChildren(childs);

                result.Add(child);
            }
            return result;
        }


        public TreeByKeyAccessor Build(IEnumerable<IVirtualFile> data)
        {
            var byKeyAccessor = new TreeByKeyAccessor();
            
            var preparedData = PrepareData(data);
            byKeyAccessor.Tree = BuildTree(preparedData, 0)[0];

            return byKeyAccessor;
        }
        
        public void BuildKeyMapping(TreeByKeyAccessor result)
        {
            var mapping = result.KeyMappings = new Dictionary<int, IList<int>>();

            var pathIterator = new PathEnumerator<FileLayerSongDo>(result.Tree);

            while (pathIterator.MoveNext())
            {
                var key = pathIterator.CurrentItem.Value.Id;
                mapping[key] = pathIterator.CurrentPath;
            }
        }
    }
}
