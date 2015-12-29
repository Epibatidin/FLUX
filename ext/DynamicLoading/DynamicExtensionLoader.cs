using System;
using Facade.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicLoading
{
    public class DynamicExtensionLoader
    {
        private readonly IConfigurationBinderFacade _configurationBinder;

        public DynamicExtensionLoader(IConfigurationBinderFacade configurationBinder)
        {
            _configurationBinder = configurationBinder;
        }


        public void LoadExtension(IConfiguration config, IServiceCollection services,
            params IDynamicLoadableExtensionConfiguration[] loadableExtensionConfigurations)
        {
            foreach (var loadableExtensionConfiguration in loadableExtensionConfigurations)
            {
                var installerType = Type.GetType(loadableExtensionConfiguration.Type);
                if (installerType == null) continue;

                var installer = Activator.CreateInstance(installerType) as IDynamicExtensionInstaller;
                if (installer == null) continue;

                installer.ConfigurationBinder = _configurationBinder;
                installer.Install(config, loadableExtensionConfiguration.SetionName, services);
            }
        }
    }
}
