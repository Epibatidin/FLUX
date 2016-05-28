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
            if (vf.PathParts.Length > level)
                return vf.PathParts[level];

            return null;
        }

        public static FileLayerSongDo BuildSong(IVirtualFile vf, int level)
        {
            var file = new FileLayerSongDo();
            
            file.LevelValue = new PartedString(vf.PathParts[level]);

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
