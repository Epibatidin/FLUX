using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IVirtualFileFactory
    {
        bool CanHandleProviderKey(string providerId);

        IVirtualFileStreamReader GetReader();

        IDictionary<int, IVirtualFile> RetrieveVirtualFiles(VirtualFileFactoryContext context);
    }
}
