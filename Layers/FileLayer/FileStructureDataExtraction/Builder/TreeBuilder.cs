using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Interfaces.VirtualFile;
using FileItem = Tree.TreeItem<FileStructureDataExtraction.FileLayerSongDO>;

namespace FileStructureDataExtraction.Builder
{
    public class TreeBuilder
    {
        public TreeBuilder()
        {
        }
             
 
        private List<Tuple<int, List<string>>> prepareData(IEnumerable<IVirtualFile> _data)
        {
            // an dieser stelle empfehle ich den baum auf eine constante größe zu bluben 
            // dh wenn parts.length < 3 
            // add CD1 
            var z = from item in _data
                    select Tuple.Create(item.ID, ConvertFileNameToTreeableData(item));

            return z.ToList();
        }

        private List<string> ConvertFileNameToTreeableData(IVirtualFile fi)
        {
            List<string> result = fi.VirtualPath.Split('\\').ToList();
            if(result.Count < 3)
                result.Add("CD1");

            result.Add(fi.Name);
            return result;
        }


        private List<FileItem> BuildTree(IEnumerable<Tuple<int, List<string>>> data, int depth)
        {
            // wenn depth == items2.length 
            // break;

            var grped = from r in
                            (from item in data
                             where item.Item2.Count > depth
                             select item)
                        group r by r.Item2[depth] into grp
                        select grp;

            List<FileItem> result = null;
            if (grped.Any())
            {
                result = new List<FileItem>();
                
                foreach (var item in grped)
                {
                    var child = new FileItem();
                    child.Level = depth;
                    var temp = new FileLayerSongDO();
                    temp.SetByDepth(depth, item.Key);
                    var current = item.First();

                    if (depth == current.Item2.Count - 1)
                        temp.ID = item.First().Item1;
                    
                    child.Value = temp;

                    var childs = BuildTree(item, depth + 1);
                    if (childs != null)
                        child.SetChildren(childs);

                    result.Add(child);
                }
            }
            return result;
        }
        

        public FileItem Build(IEnumerable<IVirtualFile> data)
        {
            var preparedData = prepareData(data);

            FileItem item = new FileItem();

            return BuildTree(preparedData, 0)[0];
        }
    }
}
