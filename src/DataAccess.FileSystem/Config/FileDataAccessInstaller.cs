using DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using DynamicLoading;

namespace DataAccess.FileSystem.Config
{
    public class FileDataAccessInstaller : DynamicExtensionInstallerBase<DirectorySourcesCollection>
    {
        public FileDataAccessInstaller()
        {
            InterfaceType = typeof (IVirtualFileRootConfiguration);
        }

        public override void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IVirtualFileFactory, DirectoryVirtualFileFactory>();
        }
    }
}
