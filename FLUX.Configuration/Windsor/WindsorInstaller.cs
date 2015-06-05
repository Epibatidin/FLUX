using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Extraction.Base;
using Extraction.Base.Config;
using Extraction.Interfaces;
using Extraction.Interfaces.Layer;
using FLUX.Configuration.Config;
using FLUX.Configuration.Windsor.Lifestyle;

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

            container.Register(Component.For<IConfigurationFactory>().ImplementedBy<ConfigurationFactory>());
            container.Register(Component.For<ConfigurationData, IExtractionLayerConfigurationProvider>()
                .ImplementedBy<ConfigurationData>()
                .UsingFactory<ConfigurationFactory, ConfigurationData>(c => c.Build()));

            Layer(container);

        }

        private void Layer(IWindsorContainer container)
        {
            container.Register(Component.For<IDataStoreProvider>().ImplementedBy<DataStoreProvider>());
            container.Register(Component.For<DataStore>().ImplementedBy<DataStore>().LifestyleScoped<PerCookieLifestyleAdapter>());
            container.Register(Component.For<IExtractionProcessorFactory>().ImplementedBy<ExtractionProcessorFactory>());
            container.Register(Component.For<IExtractionProcessor>()
                     .UsingFactory<ExtractionProcessorFactory, IExtractionProcessor>(r => r.Create()));

        }
    }

}