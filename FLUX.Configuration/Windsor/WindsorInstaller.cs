using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DataAccess.Base;
using DataAccess.Base.Config;
using DataAccess.Interfaces;
using Extension.Configuration;
using Extraction.Base;
using Extraction.Interfaces;
using Extraction.Interfaces.Layer;
using FLUX.Configuration.Windsor.Lifestyle;
using FLUX.Interfaces.Web;
using FLUX.Web.Logic;
using System.Web.Mvc;

namespace FLUX.Configuration.Windsor
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyNamed("FLUX.Web.MVC").BasedOn<Controller>()
                .LifestyleTransient()
                .Configure(component => component.Named(component.Implementation.Name.Replace("Controller", ""))));

            //container.Register(Component.For<IHttpContextProvider>().ImplementedBy<HttpContextProvider>());
            Layer(container);
            Configuration(container);
        }

        private void Layer(IWindsorContainer container)
        {
            container.Register(Component.For<IDataStoreProvider>().ImplementedBy<DataStoreProvider>());
            container.Register(Component.For<DataStore>().ImplementedBy<DataStore>().LifestyleScoped<PerCookieLifestyleAdapter>());
            container.Register(Component.For<IExtractionProcessorFactory>().ImplementedBy<ExtractionProcessorFactory>());
            container.Register(Component.For<IExtractionProcessor>()
                     .UsingFactory<ExtractionProcessorFactory, IExtractionProcessor>(r => r.Create()));
        }

        private void Configuration(IWindsorContainer container)
        {
            container.Register(Component.For<IConfigurationFormModelBuilder>().ImplementedBy<ConfigurationFormModelBuilder>());
            container.Register(Component.For<IVirtualFileConfigurationReader>().ImplementedBy<VirtualFileConfigurationReader>());
            //container.Register(Component.For<IVirtualFileAccessorSectionGroupProvider>().ImplementedBy<VirtualFileAccessorSectionGroupProvider>());

            container.Register(Component.For<IConfigurationLoader>().ImplementedBy<ConfigurationLoader>());
            
            container.Register(Component.For<ConfigurationHolderFactory>());

            container.Register(Component.For<IVirtualFileAccessorSectionGroupProvider>()
                .UsingFactory<ConfigurationHolderFactory, VirtualFileAccessorSectionGroupProvider>
                (r => r.BuildGroup<VirtualFileAccessorSectionGroupProvider, VirtualFileAccessorSectionGroup>
                    ("VirtualFileProvider", "VirtualFileAccessor")));
        }
    }

}