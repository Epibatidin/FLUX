using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using FLUXMVC.Windsor;

namespace FLUXMVC.App_Start
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
            Container.Install(new WindsorInstaller());

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(Container.Kernel));

            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}