using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.ISSC;
using Interfaces.VirtualFile;
using Tree;
using AbstractDataExtraction;
using FileItem = Tree.TreeItem<FileStructureDataExtraction.Builder.FileLayerSongDO>;
using Common;
using System.IO;
using Interfaces;

namespace FileStructureDataExtraction.Builder
{
    public class TreeBuilder
    {
        public TreeBuilder()
        {
        }
             
 
        private List<Tuple<int, List<string>>> prepareData(int constPathLength, IEnumerable<IVirtualFile> _data)
        {
            // an dieser stelle empfehle ich den baum auf eine constante größe zu bluben 
            // dh wenn parts.length < 3 
            // add CD1 
            var z = from item in _data
                    select Tuple.Create(0, ConvertFileNameToTreeableData(constPathLength, item));

            return z.ToList();
        }

        private List<string> ConvertFileNameToTreeableData(int path, IVirtualFile fi)
        {
            List<string> result = fi.VirtualPath.Substring(path + 1, fi.VirtualPath.Length - path -2- fi.Name.Length).Split('\\').ToList();
            if (result.Count < 3)
                result.Add("CD1");

            result.Add(Path.GetFileNameWithoutExtension(fi.Name));

            return result;
        }
        

        private List<FileItem> BuildTree(IEnumerable<Tuple<int,List<string>>> _data, int depth)
        {
            // wenn depth == items2.length 
            // break;

            var grped = from r in (from item in _data
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
                    child.Value = temp;
                    var childs = BuildTree(item, depth + 1);
                    if (childs != null)
                        child.SetChildren(childs);

                    result.Add(child);
                }
            }
            return result;
        }
        

        public FileItem Build(int constPathLength, IEnumerable<IVirtualFile> _data)
        {
            var preparedData = prepareData(constPathLength, _data);

            FileItem item = new FileItem();

            return BuildTree(preparedData, 0)[0];
        }
    }
}
