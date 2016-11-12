using DataAccess.Base.Config;
using DynamicLoading;
using Extraction.Base.Config;
using Facade.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FLUX.Configuration
{
    public class ApplicationStarter
    {
        protected void Startup(IConfigurationBinderFacade configurationBinder, IConfiguration config, IServiceCollection services)
        {
            var loader = new DynamicExtensionLoader(configurationBinder);

            var dataAccess = configurationBinder.Bind<VirtualFileAccessorSectionGroup>(config);
            var layerConfig = configurationBinder.Bind<ExtractionLayerConfig>(config);

            services.Add(new ServiceDescriptor(typeof(IOptions<VirtualFileAccessorSectionGroup>), new PlainOptions<VirtualFileAccessorSectionGroup>( dataAccess)));
            services.Add(new ServiceDescriptor(typeof(IOptions<ExtractionLayerConfig>), new PlainOptions<ExtractionLayerConfig>(layerConfig)));

            loader.LoadExtension(config, services, dataAccess.Sources);
            loader.LoadExtension(config, services, layerConfig.Layers);
        }

        public void Startup(IServiceCollection services, IConfiguration config)
        {
            Startup(new ConfigurationBinderFacade(), config, services);
        }
    }
}