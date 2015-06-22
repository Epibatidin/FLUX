using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Extension.Configuration;

namespace FLUX.Configuration.Windsor
{
    public class RequiresMockingInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IConfigurationLocator>()
                .Instance(new StaticConfigurationLocator(@"E:\Develop\FLUX\FLUX.Configuration")));
            //container.Register(Component.For<IConfigurationLocator>().ImplementedBy<WebConfigurationLocator>());
        }
    }
}
