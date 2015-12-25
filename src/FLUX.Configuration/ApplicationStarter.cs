using Microsoft.Extensions.DependencyInjection;

namespace FLUX.Configuration
{
    public class ApplicationStarter
    {
        public IServiceCollection Container { get; }

        public ApplicationStarter(IServiceCollection windsorContainer)
        {
            Container = windsorContainer;
        }

        //public void Setup(Assembly controllerAssembly)
        //{
        //    Container.Install(new MVCInfrastructureInstaller(controllerAssembly), new WindsorInstaller(), new RequiresMockingInstaller());

        //    ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(Container.Kernel));
        //}
    }
}