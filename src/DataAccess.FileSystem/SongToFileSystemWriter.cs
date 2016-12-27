using DataAccess.Interfaces;
using DataStructure.Tree.Builder;
using DataStructure.Tree.Iterate;
using System;
using System.Collections.Generic;
using System.IO;
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
            var tree = _treeBuilder.BuildTreeFromCollection(songs, _patternProvider.FormattedLevelValue, _patternProvider.FormattedLevelValue);

            var pathIterator = new PathEnumerator<string>(tree);

            var filePath = @"D:\FluxWorkBenchFiles\Working\Result";

            var rootFolder = new DirectoryInfo(filePath);

            while(pathIterator.MoveNext())
            {

            }


            int depth = 4;
            for (int i = 0; i < depth; i++)
            {



            }



        }


        public void Write_old(IVirtualFileStreamReader streamReader, IEnumerable<IVirtualFile> vfs, IEnumerable<IExtractionValueFacade> songs)
        {
            // 
            var filePath = @"D:\FluxWorkBenchFiles\Working\Result";

            var rootFolder = new DirectoryInfo(filePath);

            foreach (var item in songs)
            {
                var vf = vfs.First(c => c.ID == item.Id);
                
                var values = item.ToValues();

                var directory = BuildName(values);
                using (var sourceStream = streamReader.OpenStreamForReadAccess(vf))
                using (var destinationStream = CreateFileForWrite(rootFolder, directory))
                {
                    sourceStream.CopyTo(destinationStream);

                    destinationStream.Flush();
                    sourceStream.Flush();
                }
            }
        }

        private Stream CreateFileForWrite(DirectoryInfo root, IList<string> names)
        {
            var movingDir = root;

            for (int i = 0; i < names.Count -1; i++)
            {
                
                var dirs = movingDir.GetDirectories(names[i]);
                if (!dirs.Any())
                    movingDir = movingDir.CreateSubdirectory(names[i]);
                else
                    movingDir = dirs[0];
            }

            var fileStream = new FileStream(movingDir.FullName + "\\" + names.Last(), FileMode.Create ,FileAccess.Write);

            return fileStream;
        }

        private IList<string> BuildName(IList<Tuple<string, string>> values)
        {
            var list = new List<string>();

            list.Add(GetValueByKey(values, "Artist"));
            list.Add(GetValueByKey(values, "Year") + " - " + GetValueByKey(values, "Album"));
            list.Add(GetValueByKey(values, "CD"));
            list.Add(GetValueByKey(values, "TrackNr") + " - " + GetValueByKey(values, "SongName") + " - " + GetValueByKey(values, "Artist") + ".mp3");

            return list;
        }

        private string GetValueByKey(IList<Tuple<string, string>> values, string key )
        {
            return values.First(c => c.Item1 == key).Item2;
        }
    }
}
