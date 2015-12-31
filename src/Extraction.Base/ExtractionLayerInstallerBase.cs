using DynamicLoading;
using Facade.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Extraction.Base
{
    public class ExtractionLayerInstallerBase<TConfiguration> : IDynamicExtensionInstaller 
    {
        public IConfigurationBinderFacade ConfigurationBinder { get; set; }
        public void Install(IConfiguration config, string sectionName, IServiceCollection services)
        {
            


        }
    }
}
