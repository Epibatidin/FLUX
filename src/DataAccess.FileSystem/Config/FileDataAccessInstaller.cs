using DataAccess.Base;
using DataAccess.FileSystem.Directory;
using DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.FileSystem.Config
{
    public class FileDataAccessInstaller : DataAccessInstallerBase<DirectorySourcesCollection>
    {
        public override void RegisterServices(IServiceCollection serviceCollection)
        {
            //serviceCollection.AddSingleton<IVirtualFileRootConfiguration, DirectoryVirtualFileBuilder>();
        }
    }
}
