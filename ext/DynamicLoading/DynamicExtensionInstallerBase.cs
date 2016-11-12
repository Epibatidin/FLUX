using System;
using Facade.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicLoading
{
    public abstract class DynamicExtensionInstallerBase<TConfigurationSection> : IDynamicExtensionInstaller 
        where TConfigurationSection : class, new()
    {
        public IConfigurationBinderFacade ConfigurationBinder { get; set; }

        protected Type InterfaceType { get; set; }

        public void Install(IConfiguration configuration, string sectionName, IServiceCollection services)
        {
            var config = ConfigurationBinder.Bind<TConfigurationSection>(configuration, sectionName);

            var assectionHolder = config as ISectionNameHolder;
            if (assectionHolder != null)
                assectionHolder.SectionName = sectionName;

            services.Add(new ServiceDescriptor(typeof(TConfigurationSection), config));
            
            if(InterfaceType != null)
                services.Add(new ServiceDescriptor(InterfaceType, config));

            RegisterServices(services);
            RegisterServices(services, config);
        }

        public abstract void RegisterServices(IServiceCollection services);
        public virtual void RegisterServices(IServiceCollection services, TConfigurationSection config)
        {

        }
    }
}
