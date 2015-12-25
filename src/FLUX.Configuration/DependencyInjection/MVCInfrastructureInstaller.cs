using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace FLUX.Configuration.DependencyInjection
{
    public class MVCInfrastructureInstaller
    {
        private readonly Assembly _controllerAssembly;

        public MVCInfrastructureInstaller(Assembly controllerAssembly)
        {
            _controllerAssembly = controllerAssembly;
        }
        
        //public void Install(IServiceCollection container)
        //{
        //    container.Register(Classes.FromAssembly(_controllerAssembly).BasedOn<Controller>()
        //        .LifestyleTransient()
        //        .Configure(component => component.Named(component.Implementation.Name.Replace("Controller", ""))));

        //}
    }
}
