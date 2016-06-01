using Microsoft.Extensions.Configuration;

namespace Facade.Configuration
{
    public interface IConfigurationBinderFacade
    {
        TConfig Bind<TConfig>(IConfiguration configuration, string sectionName) where TConfig : class;
    }

    public class ConfigurationBinderFacade : IConfigurationBinderFacade
    {
        public TConfig Bind<TConfig>(IConfiguration configuration, string sectionName) 
            where TConfig : class => configuration.GetValue<TConfig>(sectionName);
    }
}
