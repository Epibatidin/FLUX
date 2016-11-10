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

            var dataAccess = new VirtualFileAccessorSectionGroup();
            config.Bind(dataAccess);
            
            services.AddSingleton(dataAccess);

            var layerConfig = new ExtractionLayerConfig();
            config.Bind(layerConfig);
            services.AddSingleton(layerConfig);
            //services.Configure<ExtractionLayerConfig>(config);

            loader.LoadExtension(config, services, dataAccess.Sources);
            loader.LoadExtension(config, services, layerConfig.Layers);
        }
    }
}