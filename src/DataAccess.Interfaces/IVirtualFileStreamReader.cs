using System;
using System.IO;

namespace DataAccess.Interfaces
{
    public interface IVirtualFileStreamReader
    {
        Type GetVirtualFileType();

        Stream OpenStreamForReadAccess(IVirtualFile virtualFile);
    }
}
