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
    }
}