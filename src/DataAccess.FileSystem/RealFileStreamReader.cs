using System;
using System.IO;
using DataAccess.Interfaces;

namespace DataAccess.FileSystem
{
    public class RealFileStreamReader : IVirtualFileStreamReader
    {
        public Type GetVirtualFileType() => typeof(RealFile);

        public Stream OpenStreamForReadAccess(IVirtualFile virtualFile)
        {
            return new FileStream(virtualFile.VirtualPath, FileMode.Open, FileAccess.Read);
        }
    }
}
