using DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using DynamicLoading;

namespace DataAccess.FileSystem.Config
{
    public class FileDataAccessInstaller : DynamicExtensionInstallerBase<DirectorySourcesCollection, IVirtualFileRootConfiguration>
    {
        public override void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IVirtualFileFactory, DirectoryVirtualFileFactory>();
        }
    }
}
