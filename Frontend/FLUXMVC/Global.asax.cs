using System.Web;
using Castle.Windsor;
using FLUXMVC.App_Start;

namespace FLUXMVC
{
    public class MvcApplication : HttpApplication
    {
        private static ApplicationStarter _applicationStarter;
        
        protected void Application_Start()
        {
            _applicationStarter = new ApplicationStarter(new WindsorContainer());
            _applicationStarter.Setup();
        }
    }
}