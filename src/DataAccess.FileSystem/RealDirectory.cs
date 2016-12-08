using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataAccess.Interfaces;

namespace DataAccess.FileSystem
{
    [System.Diagnostics.DebuggerStepThrough]
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

        public string GetFile(string name)
        {
            var files = _di.GetFiles(name);

            return files[0].FullName;
        }
        
        public IEnumerable<string> GetFiles()
        {
            return GetFiles("*", false);
        }

        public IEnumerable<string> GetFiles(string searchPattern)
        {
            return GetFiles(searchPattern, false);
        }

        public IEnumerable<string> GetFiles(string searchPattern, bool deepSearch)
        {
            return _di.EnumerateFiles(searchPattern, DeepToEnum(deepSearch)).Select(c => c.FullName);
        }

        private SearchOption DeepToEnum(bool deepSearch)
        {
            return deepSearch ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
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
