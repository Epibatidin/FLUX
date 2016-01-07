using System;
using Extraction.Base.Config;
using Extraction.Layer.File.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.OptionsModel;
using Xunit;
using Assert = NUnit.Framework.Assert;
using Is = NUnit.Framework.Is;

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

        [Fact]
        public void should_can_read_config_file()
        {
            var build = RetrieveFromConfig<TConfigurationRootType>();

            Assert.That(build.Value, Is.Not.Null);
        }
    }

    public class LayerConfigTests2 : ConfigurationTestBase<ExtractionLayerConfig>
    {
        public LayerConfigTests2() : base(@"D:\Develop\FLUX\src\FLUX.Configuration\Files\Layer.json", null)
        {

        }

        [Fact]
        public void should_not_be_async()
        {
            var bound = RetrieveFromConfig(c => c.ASync);

            Assert.That(bound, Is.False);
        }

        [Fact]
        public void should_can_read_layer_items()
        {
            var bound = RetrieveFromConfig(c => c.Layers);
                
            Assert.That(bound.Length, Is.EqualTo(2));
        }


        [Fact]
        public void should_not_be_empty_item()
        {
            var bound = RetrieveFromConfig(c => c.Layers[0]);

            Assert.That(bound.Type, Is.Not.Empty);
            Assert.That(bound.Active, Is.True);
            Assert.That(bound.ExtractorType, Is.Not.Empty);
        }
    }

    public class LayerConfigTests : ConfigurationTestBase<FileLayerConfig>
    {
        public LayerConfigTests() : base(@"D:\Develop\FLUX\src\FLUX.Configuration\Files\Layer.json", "File")
        {
           
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
