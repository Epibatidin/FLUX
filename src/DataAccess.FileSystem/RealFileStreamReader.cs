using System.IO;
using DataAccess.Interfaces;

namespace DataAccess.FileSystem
{
    public class RealFileStreamReader : IVirtualFileStreamReader
    {
        public Stream OpenStreamForReadAccess(VirtualFile virtualFile)
        {
            return new FileStream(virtualFile.VirtualPath, FileMode.Open, FileAccess.Read);
        }
    }
}
