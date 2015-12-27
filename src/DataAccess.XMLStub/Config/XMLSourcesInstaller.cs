using DataAccess.Base;
using DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.XMLStub.Config
{
    public class XMLSourcesInstaller : DataAccessInstallerBase<XMLSourcesCollection>
    {
        public override void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IVirtualFileFactory, XmlVirtualFileFactory>();
        }
    }
}
