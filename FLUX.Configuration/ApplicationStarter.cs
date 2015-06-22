using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using FLUX.Configuration.Windsor;

namespace FLUX.Configuration
{
    public class ApplicationStarter
    {
        public IWindsorContainer Container { get; private set; }

        public ApplicationStarter(IWindsorContainer windsorContainer)
        {
            Container = windsorContainer;
        }

        public void Setup()
        {
            Container.Install(new WindsorInstaller(), new RequiresMockingInstaller());

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(Container.Kernel));

            
        }
    }
}