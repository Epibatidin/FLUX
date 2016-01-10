using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IVirtualFileConfigurationReader
    {
        AvailableVirtualFileProviderDo ReadToDO();

        IDictionary<int, IVirtualFile> GetVirtualFiles(string selectedSource, string activeProviderGrp);

        IVirtualFileStreamReader RetrieveReader(string activeProviderGrp);
    }
}
