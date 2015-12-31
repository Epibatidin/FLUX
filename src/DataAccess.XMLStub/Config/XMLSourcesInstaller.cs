using DataAccess.Interfaces;
using DynamicLoading;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.XMLStub.Config
{
    public class XMLSourcesInstaller : DynamicExtensionInstallerBase<XMLSourcesCollection>
    {
        public XMLSourcesInstaller()
        {
            InterfaceType = typeof(IVirtualFileRootConfiguration);
        }

        public override void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IVirtualFileFactory, XmlVirtualFileFactory>();
        }
    }
}
