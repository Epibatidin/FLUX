using System.IO;

namespace DataAccess.Interfaces
{
    public interface IVirtualFileStreamReader
    {
        Stream OpenStreamForReadAccess(IVirtualFile virtualFile);
    }
}
