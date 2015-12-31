using DataAccess.Base.Config;
using DynamicLoading;
using Extraction.Base.Config;
using Facade.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FLUX.Configuration
{
    public class ApplicationStarter
    {
        public void Startup(IServiceCollection services, IConfiguration config)
        {
            var loader = new DynamicExtensionLoader(new ConfigurationBinderFacade());

            var dataAccess = config.Get<VirtualFileAccessorSectionGroup>();
            services.Configure<VirtualFileAccessorSectionGroup>(config);

            var layerConfig = config.Get<ExtractionLayerConfig>();
            services.Configure<ExtractionLayerConfig>(config);

            loader.LoadExtension(config, services, dataAccess.Sources);
            loader.LoadExtension(config, services, layerConfig.Layers);
        }
    }
}