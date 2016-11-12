using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FLUX.Configuration.DependencyInjection
{
    public class FrameworkInstaller
    {
        public void Install(IServiceCollection container)
        {
            container.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            container.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }
    }
}
