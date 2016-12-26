using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface ISongToFileSystemWriter
    {
        void Write(IVirtualFileStreamReader streamReader, IEnumerable<IVirtualFile> vfs, IEnumerable<IExtractionValueFacade> songs)
    }
}
