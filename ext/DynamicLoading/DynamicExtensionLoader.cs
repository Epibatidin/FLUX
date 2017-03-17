using System;
using Facade.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.Loader;
using System.Reflection;

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
                if(!loadableExtensionConfiguration.Active) continue;
                var installer = LoadInstaller(loadableExtensionConfiguration.Type); 
                if (installer == null) continue;

                installer.ConfigurationBinder = _configurationBinder;
                installer.Install(config, loadableExtensionConfiguration.SetionName, services);
            }
        }

        private IDynamicExtensionInstaller LoadInstaller(string type)
        {
            var firstComma = type.IndexOf(',');
            var assemblyNameString = type.Substring(firstComma +1);
            var assamblyNAme = new AssemblyName(assemblyNameString);
            var assambly = Assembly.Load(assamblyNAme);
            
            var installertype = Type.GetType(type);
            if (installertype == null) return null;
            return Activator.CreateInstance(installertype) as IDynamicExtensionInstaller;
        }

        //private IDynamicExtensionInstaller LoadInstaller(string type)
        //{

        //    var installertype = Type.GetType(type);
        //    if (installertype == null) return null;
        //    return Activator.CreateInstance(installertype) as IDynamicExtensionInstaller;
        //}
    }
}
