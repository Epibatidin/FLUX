using DataAccess.Interfaces;
using Extension.Test;
using FLUX.DomainObjects;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace FLUX.Web.Logic.Tests
{
    public class ConfigurationFormModelBuilderTests : FixtureBase<ConfigurationFormModelBuilder>
    {
        private Mock<IVirtualFileConfigurationReader> _configurationProvider;

        protected override void Customize()
        {

            _configurationProvider = Fixture.Freeze<Mock<IVirtualFileConfigurationReader>>();

        }

        [Test]
        public void should_return_new_formmodel()
        {
            var model = SUT.BuildFormModel();
            Assert.That(model, Is.Not.Null);
            Assert.That(model, Is.TypeOf<ConfigurationFormModel>());
        }

        [Test]
        public void should_get_available_virtual_file_provider_from_configuration_provider()
        {
            var providerDO = Fixture.Create<AvailableVirtualFileProviderDO>();
            _configurationProvider.Setup(c => c.ReadToDO()).Returns(providerDO);

            var model = SUT.BuildFormModel();

            _configurationProvider.Verify(c => c.ReadToDO());
        }

        [Test]
        public void should_copy_available_provider_names_from_configuration_provider_result()
        {
            var providerDO = Fixture.Create<AvailableVirtualFileProviderDO>();
            _configurationProvider.Setup(c => c.ReadToDO()).Returns(providerDO);

            var model = SUT.BuildFormModel();

            Assert.That(model.VirtualFileProvider.CurrentProviderName, Is.EqualTo(providerDO.CurrentProviderName));
            Assert.That(model.VirtualFileProvider.ProviderNames, Is.EqualTo(providerDO.ProviderNames));
        }
    }
}
