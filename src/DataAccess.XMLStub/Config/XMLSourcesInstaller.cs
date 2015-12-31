using DataAccess.Interfaces;
using DynamicLoading;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.XMLStub.Config
{
    public class XMLSourcesInstaller : DynamicExtensionInstallerBase<XMLSourcesCollection, IVirtualFileRootConfiguration>
    {
        public override void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IVirtualFileFactory, XmlVirtualFileFactory>();
        }
    }
}
