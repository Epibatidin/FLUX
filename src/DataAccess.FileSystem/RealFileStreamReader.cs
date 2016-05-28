using System;
using System.IO;
using DataAccess.Interfaces;

namespace DataAccess.FileSystem
{
    public class RealFileStreamReader : IVirtualFileStreamReader
    {
        private readonly string _rootDir;

        public RealFileStreamReader(string rootDir)
        {
            _rootDir = rootDir;
        }
        
        public Stream OpenStreamForReadAccess(IVirtualFile virtualFile)
        {
            return null;

            //return new FileStream(string.Concat(_rootDir,virtualFile.VirtualPath), FileMode.Open, FileAccess.Read);
        }
    }
}
