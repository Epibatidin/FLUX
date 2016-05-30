using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;
using DataStructure.Tree;
using DataStructure.Tree.Builder;
using DataStructure.Tree.Iterate;
using Extraction.DomainObjects.StringManipulation;

namespace Extraction.Layer.File
{
    public class TreeByKeyAccessorBuilder : ITreeByKeyAccessorBuilder
    {
        private readonly ITreeBuilder _treeBuilder;

        public TreeByKeyAccessorBuilder(ITreeBuilder treeBuilder)
        {
            _treeBuilder = treeBuilder;
        }

        public static string GetKeyOnVirtualFile(IVirtualFile vf, int level)
        {
            return GetValueByDepthWithCDDummy(vf.PathParts, level);
        }

        public static string GetValueByDepthWithCDDummy(string[] pathParts, int level)
        {
            int index = level;
            
            if (pathParts.Length < 4)
            {
                if (level == 2) return "CD1";

                if (level > 2) --index;
            }

            if (pathParts.Length > index)
                return pathParts[index];

            return null;
        }

        public static FileLayerSongDo BuildSong(IVirtualFile vf, int level)
        {
            var value = GetValueByDepthWithCDDummy(vf.PathParts, level);
            if (value == null)
                return null;
                //throw new Exception("WTF");

            var file = new FileLayerSongDo();
            
            file.LevelValue = new PartedString(value);

            return file;
        }

        public TreeByKeyAccessor Build(IEnumerable<IVirtualFile> data)
        {
            var byKeyAccessor = new TreeByKeyAccessor();

            byKeyAccessor.Tree = _treeBuilder.BuildTreeFromCollection(data, GetKeyOnVirtualFile, BuildSong);
            
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

