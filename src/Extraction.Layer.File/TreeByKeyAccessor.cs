using System.Collections.Generic;
using DataStructure.Tree;
using Extraction.Base;
using Extraction.Interfaces;

namespace Extraction.Layer.File
{
    public class TreeByKeyAccessor : ISongByKeyAccessor
    {
        public TreeItem<FileLayerSongDo> Tree { get; set; }
        public IDictionary<int, IList<int>> KeyMappings { get; set; }
        
        public ISong GetByKey(int key)
        {
            var path = KeyMappings[key];
            var root = Tree;

            var fls = new NonOverwrittingSongDummy();

            foreach (var digit in path)
            {
                fls.Add(root.Value);
                root = root[digit];
            }
            fls.Add(root.Value);
            return fls;
        }
    }
}
