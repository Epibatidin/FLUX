using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataAccess.Interfaces;

namespace DataAccess.FileSystem
{
    public class RealDirectory : IVirtualDirectory
    {
        private readonly DirectoryInfo _di;

        public RealDirectory(string path)
        {
            _di = new DirectoryInfo(path);
        }

        public RealDirectory(DirectoryInfo di)
        {
            _di = di;
        }

        public string DirectoryName => _di.Name;

        public IVirtualFile GetFile(string name)
        {
            var files = _di.GetFiles(name);
            return new RealFile(files[0]);
        }

        public IEnumerable<IVirtualFile> GetFiles()
        {
            return GetFiles("*", false);
        }

        public IEnumerable<IVirtualFile> GetFiles(string searchPattern)
        {
            return GetFiles(searchPattern, false);
        }

        public IEnumerable<IVirtualFile> GetFiles(string searchPattern, bool deepSearch)
        {
            return _di.EnumerateFiles(searchPattern, DeepToEnum( deepSearch)).Select(c => new RealFile(c));
        }

        private SearchOption DeepToEnum(bool deepSearch)
        {
            return deepSearch ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
        }

        public IEnumerable<IVirtualFile> GetFiles(string searchPattern, bool deepSearch, Func<int, int> idGenerator)
        {
            int pos = 0;
            return _di.EnumerateFiles(searchPattern, DeepToEnum(deepSearch)).Select(c => new RealFile(c, idGenerator(++pos)));
        }

        public IVirtualDirectory GetDirectory(string directoryName)
        {
            var items = _di.GetDirectories(directoryName);

            return new RealDirectory(items[0]);
        }

        public IEnumerable<IVirtualDirectory> GetDirectories()
        {
            return GetDirectories("*", false);
        }

        public IEnumerable<IVirtualDirectory> GetDirectories(string searchPattern)
        {
            return GetDirectories(searchPattern, false);
        }

        public IEnumerable<IVirtualDirectory> GetDirectories(string searchPattern, bool deepSearch)
        {
            return _di.EnumerateDirectories(searchPattern, DeepToEnum( deepSearch)).Select(c => new RealDirectory(c));
        }
    }
}
