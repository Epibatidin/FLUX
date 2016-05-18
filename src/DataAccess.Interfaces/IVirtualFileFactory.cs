using System;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IVirtualFileFactory
    {
        bool CanHandleProviderKey(string providerId);

        Type GetVirtualFileArrayType();

        IVirtualFileStreamReader GetReader(VirtualFileFactoryContext context);

        IList<IVirtualFile> RetrieveVirtualFiles(VirtualFileFactoryContext context);
    }
}
