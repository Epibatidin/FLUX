using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FLUX.Configuration.Tests.Config
{
    public class ConfigurationTestBase<TConfigurationRootType> where TConfigurationRootType : class , new()
    {
        protected readonly IConfiguration Config;

        public ConfigurationTestBase(string filePath, string section)
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile(filePath);

            IConfiguration config = configBuilder.Build();

            if (section != null)
                config = config.GetSection(section);

            Config = config;
        }
        
        protected OptionsManager<TConfig> RetrieveFromConfig<TConfig>() where TConfig : class, new()
        {
            return RetrieveFromConfig<TConfig>(Config);
        }

        protected OptionsManager<TConfig> RetrieveFromConfig<TConfig>(IConfiguration configuration) where TConfig : class, new()
        {
            var option = new ConfigureFromConfigurationOptions<TConfig>(configuration);
            return new OptionsManager<TConfig>(new[] { option });
        }

        protected TProperty RetrieveFromConfig<TProperty>(Func<TConfigurationRootType, TProperty> propertyAccesscor)
        {
            var optManager = RetrieveFromConfig<TConfigurationRootType>();
            return propertyAccesscor(optManager.Value);
        }
    }
}