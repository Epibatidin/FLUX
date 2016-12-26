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

        public override void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IVirtualFileFactory, DirectoryVirtualFileFactory>();

            services.AddSingleton<IPatternProvider, PatternProvider>();
            services.AddSingleton<ISongToFileSystemWriter, SongToFileSystemWriter>();
        }
    }
}
