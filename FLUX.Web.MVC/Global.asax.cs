using System.Web.Routing;
using Castle.Windsor;
using FLUX.Configuration;
using FLUX.Web.MVC.App_Start;

namespace FLUX.Web.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static ApplicationStarter _applicationStarter;

        protected void Application_Start()
        {
            _applicationStarter = new ApplicationStarter(new WindsorContainer());
            _applicationStarter.Setup();

            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}