//using System;
//using System.Collections.Generic;
//using System.Linq;
//using DataAccess.Interfaces;
//using Extension.Tree;

//namespace Extraction.Layer.File
//{
//    public class TreeBuilder
//    {
//        public TreeBuilder()
//        {
//        }
             
//        private List<Tuple<int, List<string>>> PrepareData(IEnumerable<IVirtualFile> data)
//        {
//            // an dieser stelle empfehle ich den baum auf eine constante größe zu bluben 
//            // dh wenn parts.length < 3 
//            // add CD1 
//            var z = from item in data
//                    select Tuple.Create(item.ID, ConvertFileNameToTreeableData(item));

//            return z.ToList();
//        }

//        private List<string> ConvertFileNameToTreeableData(IVirtualFile fi)
//        {
//            List<string> result = fi.VirtualPath.Split('\\').ToList();
//            if(result.Count < 3)
//                result.Add("CD1");

//            result.Add(fi.Name);
//            return result;
//        }


//        private List<TreeItem<FileLayerSongDo>> BuildTree(IEnumerable<Tuple<int, List<string>>> data, int depth)
//        {
//            // wenn depth == items2.length 
//            // break;

//            var grped = from r in
//                            (from item in data
//                             where item.Item2.Count > depth
//                             select item)
//                        group r by r.Item2[depth] into grp
//                        select grp;

//            List<TreeItem<FileLayerSongDo>> result = null;
//            if (grped.Any())
//            {
//                result = new List<TreeItem<FileLayerSongDo>>();
                
//                foreach (var item in grped)
//                {
//                    var child = new TreeItem<FileLayerSongDo>();
//                    child.Level = depth;
//                    var temp = new FileLayerSongDo();
//                    temp.SetByDepth(depth, item.Key);
//                    var current = item.First();

//                    if (depth == current.Item2.Count - 1)
//                        temp.ID = item.First().Item1;
                    
//                    child.Value = temp;

//                    var childs = BuildTree(item, depth + 1);
//                    if (childs != null)
//                        child.SetChildren(childs);

//                    result.Add(child);
//                }
//            }
//            return result;
//        }
        

//        public TreeItem<FileLayerSongDo> Build(IEnumerable<IVirtualFile> data)
//        {
//            var preparedData = PrepareData(data);

//            return BuildTree(preparedData, 0)[0];
//        }
//    }
//}
