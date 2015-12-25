using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DataAccess.Base.Config;
using DataAccess.Interfaces;
using Facade.Configuration;

namespace FLUX.Configuration.Config
{
    public class VirtualFileAccessorSectionGroupReader
    {
        public void Startup(IServiceCollection services, IConfiguration config)
        {
            var section = config.Get<VirtualFileAccessorSectionGroup>();

            services.Configure<VirtualFileAccessorSectionGroup>(config);
            
            foreach (var sourceItem in section.Sources)
            {
                var installerType = Type.GetType(sourceItem.Type);
                if(installerType == null) continue;

                var installer = Activator.CreateInstance(installerType) as IDataAccessInstaller;
                if(installer == null) continue;

                installer.ConfigurationBinder = new ConfigurationBinderFacade();

                installer.Configure(config, sourceItem.SetionName, services);
            }
            
            
            //services.Configure<AppSettings>(config.GetSubKey("AppSettings"));
            // okay soweit 
            // jetzt den pfad zu sources reflectieren
            
            // iterate the sources 


            //var r = s;
        }



    }
}
