using DataAccess.Interfaces;
using DataStructure.Tree.Builder;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.FileSystem
{
    public class SongToFileSystemWriter : ISongToFileSystemWriter
    {
        IPatternProvider _patternProvider;
        ITreeBuilder _treeBuilder;

        public SongToFileSystemWriter(IPatternProvider patternProvider, ITreeBuilder treeBuilder)
        {
            _patternProvider = patternProvider;
            _treeBuilder = treeBuilder;
        }

        public void Write(IVirtualFileStreamReader streamReader, IEnumerable<IVirtualFile> vfs, IEnumerable<IExtractionValueFacade> songs)
        {
            var writer = new RealFileStreamWriter();

            var vfsDict = vfs.ToDictionary(c => c.ID, c => c);

            foreach (var song in songs)
            {
                var vf = vfsDict[song.Id];

                var pathParts = _patternProvider.CreatePathParts(song);

                using (var readStream = streamReader.OpenStreamForReadAccess(vf))
                using (var writeStream = writer.OpenForWriteAccess(pathParts, vf.Extension))
                {
                    readStream.CopyTo(writeStream);

                    readStream.Flush();
                    writeStream.Flush();
                }
            }
        }     
    }
}
