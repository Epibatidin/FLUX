using DataAccess.Base;
using DataAccess.Interfaces;
using DataStructure.Tree.Builder;
using Extraction.Base.Processor;
using Extraction.Interfaces.Layer;
using Facade.Configuration;
using Facade.MVC;
using Facade.Session;
using FLUX.Interfaces;
using FLUX.Interfaces.Web;
using FLUX.Web.Logic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FLUX.Configuration.DependencyInjection
{
    public class DependencyInstaller
    {
        public void Install(IServiceCollection container, IConfigurationRoot configurationRoot)
        {
            Layer(container);
            Configuration(container, configurationRoot);
            RegisterHelpers(container);
            RegisterBusinessComponents(container);
        }

        private void RegisterBusinessComponents(IServiceCollection container)
        {
            container.AddSingleton<IModelBinderFacade, ModelBinderFacade>();
            container.AddSingleton<ISessionFacade, SessionFacade>();

            container.AddSingleton<IExtractionContextBuilder, ExtractionContextBuilder>();
            container.AddSingleton<IConfigurationFormProcessor, ConfigurationFormProcessor>();
            container.AddSingleton<ILayerResultJoiner, LayerResultJoiner>();
        }

        public void RegisterHelpers(IServiceCollection container)
        {
            container.AddSingleton<ITreeBuilder, TreeBuilder>();
            container.AddSingleton<IVirtualFilePeristentHelper, VirtualFilePeristentHelper>();
            container.AddSingleton<IPostbackHelper, PostbackHelper>();
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