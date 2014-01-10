using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Interfaces.FileSystem;
using Interfaces.VirtualFile;

namespace VirtualFileProvider.Tests.Directory
{
    public class MockDir : IVirtualDirectory
    {
        public List<IVirtualDirectory> SubDirs { get; set; }

        public int FileCount { get; set; }
        public string DirectoryName { get; set; }
        public IVirtualFile GetFile(string name) { return new MockFile(0); }

        public IEnumerable<IVirtualFile> GetFiles()
        {
            return GetFiles("*", SearchOption.TopDirectoryOnly, idGen);
        }

        public IEnumerable<IVirtualFile> GetFiles(string searchPattern)
        {
            return GetFiles(searchPattern, SearchOption.TopDirectoryOnly, idGen);
        }

        private int idGen(int id)
        {
            return id;
        }

        public IEnumerable<IVirtualFile> GetFiles(string searchPattern, SearchOption searchOption)
        {
            return GetFiles(searchPattern, searchOption, idGen);
        }

        public IEnumerable<IVirtualFile> GetFiles(string searchPattern, SearchOption searchOption, Func<int, int> idGenerator)
        {
            int pos = 0;
            return Enumerable.Range(0, FileCount).Select(c => new MockFile(idGenerator(++pos)));
        }

        public IVirtualDirectory GetDirectory(string name)
        {
            return SubDirs[0];
        }

        public IEnumerable<IVirtualDirectory> GetDirectories()
        {
            return SubDirs;
        }

        public IEnumerable<IVirtualDirectory> GetDirectories(string searchPattern)
        {
            return SubDirs;
        }

        public IEnumerable<IVirtualDirectory> GetDirectories(string searchPattern, SearchOption searchOption)
        {
            return SubDirs;
        }
    }
}
