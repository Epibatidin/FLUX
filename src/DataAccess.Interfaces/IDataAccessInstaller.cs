using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Facade.Configuration;

namespace DataAccess.Interfaces
{
    public interface IDataAccessInstaller
    {
        IConfigurationBinderFacade ConfigurationBinder { get; set; }

        void Configure(IConfiguration configuration, string sectionName, IServiceCollection services);
    }
}
