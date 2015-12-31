using Facade.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicLoading
{
    public abstract class DynamicExtensionInstallerBase<TConfigurationSection, TConfigSectionInterface> : IDynamicExtensionInstaller where TConfigurationSection : class, TConfigSectionInterface
    {
        public IConfigurationBinderFacade ConfigurationBinder { get; set; }
        
        public void Install(IConfiguration configuration, string sectionName, IServiceCollection services)
        {
            var config = ConfigurationBinder.Bind<TConfigurationSection>(configuration, sectionName);

            var assectionHolder = config as ISectionNameHolder;
            if (assectionHolder != null)
                assectionHolder.SectionName = sectionName;

            services.Add(new ServiceDescriptor(typeof(TConfigurationSection), config));
            services.Add(new ServiceDescriptor(typeof(TConfigSectionInterface), config));

            RegisterServices(services);
        }
        
        public abstract void RegisterServices(IServiceCollection serviceCollection);
    }
}
