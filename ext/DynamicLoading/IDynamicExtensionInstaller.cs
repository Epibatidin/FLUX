using Facade.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicLoading
{
    public interface IDynamicExtensionInstaller
    {
        IConfigurationBinderFacade ConfigurationBinder { get; set; }
        void Install(IConfiguration config, string sectionName, IServiceCollection services);
    }
}