using Microsoft.Extensions.Configuration;

namespace Facade.Configuration
{
    public interface IConfigurationBinderFacade
    {
        TConfig Bind<TConfig>(IConfiguration configuration) where TConfig : class, new();
        TConfig Bind<TConfig>(IConfiguration configuration, string sectionName) where TConfig : class, new();
    }

    public class ConfigurationBinderFacade : IConfigurationBinderFacade
    {
        public TConfig Bind<TConfig>(IConfiguration configuration)
            where TConfig : class, new()
        {
            var instance = new TConfig();
            configuration.Bind(instance);
            return instance;
        }

        public TConfig Bind<TConfig>(IConfiguration configuration, string sectionName)
            where TConfig : class, new()
        {
            var subConfig = configuration.GetSection(sectionName);

            return Bind<TConfig>(subConfig);
        }
    }
}
