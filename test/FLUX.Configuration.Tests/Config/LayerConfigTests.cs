using System;
using Extraction.Layer.File.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.OptionsModel;
using Xunit;
using Assert = NUnit.Framework.Assert;
using Is = NUnit.Framework.Is;

namespace FLUX.Configuration.Tests.Config
{
    public class LayerConfigTests 
    {
        IConfiguration _config;

        public LayerConfigTests()
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile(@"D:\Develop\FLUX\src\FLUX.Configuration\Files\Layer.json");

            _config = configBuilder.Build();
        }

        private OptionsManager<TConfig> RetrieveFromConfig<TConfig>() where TConfig : class, new()
        {
            var option = new ConfigureFromConfigurationOptions<TConfig>(_config);
            return new OptionsManager<TConfig>(new[] { option });
        }

        private TProperty RetrieveFromConfig<TProperty>(Func<FileLayerConfig, TProperty> propertyAccesscor) where TProperty : class
        {
            var optManager = RetrieveFromConfig<FileLayerConfig>();
            return propertyAccesscor(optManager.Value);
        }

        [Fact]
        public void should_can_read_config_file()
        {
            var build = RetrieveFromConfig<FileLayerConfig>();

            Assert.That(build.Value, Is.Not.Null);
        }

        [Fact]
        public void should_can_read_blacklist()
        {
            var blacklist = RetrieveFromConfig(c => c.BlackList);

            Assert.That(blacklist.Count, Is.GreaterThan(0));
        }

        [Fact]
        public void should_can_read_whitelist()
        {
            var blacklist = RetrieveFromConfig(c => c.WhiteList);

            Assert.That(blacklist.Count, Is.GreaterThan(0));
        }
    }
}
