using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using Castle.MicroKernel;

namespace FLUX.Configuration.Windsor
{
    public class WindsorControllerFactory : IControllerFactory
    {
        private readonly IKernel _kernel;

        public WindsorControllerFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            return _kernel.HasComponent(controllerName) ? _kernel.Resolve<IController>(controllerName) : null;
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }

        public void ReleaseController(IController controller)
        {
            _kernel.ReleaseComponent(controller);
        }
    }
}