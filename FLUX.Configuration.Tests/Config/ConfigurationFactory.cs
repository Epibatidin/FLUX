using Extension.Test;
using FLUX.Configuration.Config;
using FLUX.Interfaces.Configuration;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace FLUX.Configuration.Tests.Config
{
    public class ConfigurationFactoryTests : FixtureBase<ConfigurationFactory>
    {
        private Mock<IConfigurationLocator> _configurationLocator;

        protected override void Customize()
        {
            _configurationLocator = Fixture.Freeze<Mock<IConfigurationLocator>>();
        }

        //[Test]
        //public void should_invoke_locator_with_extractionlayer_config_name()
        //{
        //    var config = SUT.Build();

        //    _configurationLocator.Verify(c => c.Locate("Layer"));
        //}
    }
}
