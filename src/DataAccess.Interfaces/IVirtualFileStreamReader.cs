using System;
using System.IO;

namespace DataAccess.Interfaces
{
    public interface IVirtualFileStreamReader
    {
        Type GetVirtualFileArrayType();

        Stream OpenStreamForReadAccess(IVirtualFile virtualFile);
    }
}
