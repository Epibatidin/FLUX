﻿using System.Collections.Generic;
using System.Linq;
using DataStructure.Tree;
using Extraction.Interfaces;

namespace Extraction.Layer.File
{
    public class TreeByKeyAccessor : ISongByKeyAccessor
    {
        public TreeItem<FileLayerSongDo> Tree { get; set; }
        public IDictionary<int, string> KeyMappings { get; set; }
        
        public ISong GetByKey(int key)
        {
            var path = KeyMappings[key];
            var root = Tree;

            var fls = new FileLayerSongDo();

            foreach (var digit in path.Split('-').Select(int.Parse))
            {
                root = root[digit];

                
            }
            return fls;
        }
    }
}
