using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Interfaces.FileSystem;
using Interfaces.VirtualFile;

namespace Common.FileSystem
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

        public string DirectoryName
        {
            get { return _di.Name; }
        }

        public IVirtualFile GetFile(string name)
        {
            var files = _di.GetFiles(name);
            return new RealFile( files[0]);
        }
        
        public IEnumerable<IVirtualFile> GetFiles()
        {
            return GetFiles("*", SearchOption.TopDirectoryOnly);
        }

        public IEnumerable<IVirtualFile> GetFiles(string searchPattern)
        {
            return GetFiles(searchPattern, SearchOption.TopDirectoryOnly);
        }

        public IEnumerable<IVirtualFile> GetFiles(string searchPattern, SearchOption searchOption)
        {
            return _di.EnumerateFiles(searchPattern, searchOption).Select(c => new RealFile(c));
        }

        public IEnumerable<IVirtualFile> GetFiles(string searchPattern, SearchOption searchOption, Func<int, int> idGenerator)
        {
            int pos = 0;
            return _di.EnumerateFiles(searchPattern, searchOption).Select(c => new RealFile(c, idGenerator(++pos)));
        }

        public IVirtualDirectory GetDirectory(string directoryName)
        {
            var items = _di.GetDirectories(directoryName);
          
            return new RealDirectory(items[0]);
        }

        public IEnumerable<IVirtualDirectory> GetDirectories()
        {
            return GetDirectories("*", SearchOption.TopDirectoryOnly);
        }

        public IEnumerable<IVirtualDirectory> GetDirectories(string searchPattern)
        {
            return GetDirectories(searchPattern, SearchOption.TopDirectoryOnly); ;
        }

        public IEnumerable<IVirtualDirectory> GetDirectories(string searchPattern, SearchOption searchOption)
        {
            return _di.EnumerateDirectories(searchPattern, searchOption).Select(c => new RealDirectory(c));
        }



       
    }
}
