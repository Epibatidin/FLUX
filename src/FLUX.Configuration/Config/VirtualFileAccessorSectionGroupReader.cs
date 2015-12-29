using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DataAccess.Base.Config;
using DynamicLoading;
using Facade.Configuration;

namespace FLUX.Configuration.Config
{
    public class VirtualFileAccessorSectionGroupReader
    {
        public void Startup(IServiceCollection services, IConfiguration config)
        {
            var section = config.Get<VirtualFileAccessorSectionGroup>();

            services.Configure<VirtualFileAccessorSectionGroup>(config);
            
            var loader = new DynamicExtensionLoader(new ConfigurationBinderFacade());

            loader.LoadExtension(config, services, section.Sources);
            


        }



    }
}
