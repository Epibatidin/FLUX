using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IVirtualFileFactory
    {
        bool CanHandleProviderKey(string providerId);

        IDictionary<int, IVirtualFile> RetrieveVirtualFiles(VirtualFileFactoryContext context);
    }
}
