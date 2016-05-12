using System.ComponentModel;
using FLUX.Interfaces.Web;
using FLUX.Web.Logic;
using DataAccess.Interfaces;
using DataAccess.Base;
using Extraction.Base.Processor;
using Extraction.Interfaces;
using Extraction.Interfaces.Layer;
using Microsoft.Extensions.Configuration;
using Facade.Configuration;
using Facade.MVC;
using Facade.Session;
using Microsoft.Extensions.DependencyInjection;
using FLUX.Interfaces;
using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Http.Features.Internal;

namespace FLUX.Configuration.DependencyInjection
{
    public class DependencyInstaller
    {
        public void Install(IServiceCollection container, IConfigurationRoot configurationRoot)
        {
            Layer(container);
            Configuration(container, configurationRoot);

            RegisterBusinessComponents(container);
        }

        private void RegisterBusinessComponents(IServiceCollection container)
        {
            container.AddSingleton<IPostbackHelper, PostbackHelper>();
            container.AddSingleton<IModelBinderFacade, ModelBinderFacade>();
            container.AddSingleton<ISessionFacade, SessionFacade>();
            container.AddSingleton<IVirtualFilePeristentHelper, VirtualFilePeristentHelper>();

            container.AddSingleton<IConfigurationFormProcessor, ConfigurationFormProcessor>();
        }


        public void Layer(IServiceCollection container)
        {
            container.AddSingleton<IExtractionProcessor, SequentielExtractionProcessor>();
        }

        private void Configuration(IServiceCollection container, IConfigurationRoot configurationRoot)
        {
            container.AddSingleton<IVirtualFileConfigurationReader, VirtualFileConfigurationReader>();
            container.AddSingleton<IConfigurationBinderFacade, ConfigurationBinderFacade>();

            //container.AddSingleton<AvailableVirtualFileProviderDo, AvailableVirtualFileProviderDo>();

            // starten 
            // 
            // ding installieren 
            // welches ding ? die sources configuration
            // also ding das jetzt ausgeführt wird 
            // aus der config das VirtualFileSectionGroup lesen
            // mit dem pfad nach sources im ConfigBuilder mit get<>(pfad) die einzelen source configs registrieren 
            // deleation 
            // ruf einen 
            // okay aber soweit kann ich erstmal umsetzen 



            ////container.Register(Component.For<IVirtualFileAccessorSectionGroupProvider>().ImplementedBy<VirtualFileAccessorSectionGroupProvider>());

            //container.Register(Component.For<IConfigurationLoader>().ImplementedBy<ConfigurationLoader>());

            //container.Register(Component.For<ConfigurationHolderFactory>());

            //container.Register(Component.For<IVirtualFileAccessorSectionGroupProvider>()
            //    .UsingFactory<ConfigurationHolderFactory, VirtualFileAccessorSectionGroupProvider>
            //    (r => r.BuildGroup<VirtualFileAccessorSectionGroupProvider, VirtualFileAccessorSectionGroup>
            //        ("VirtualFileProvider", "VirtualFileAccessor")));
        }
    }


   

}