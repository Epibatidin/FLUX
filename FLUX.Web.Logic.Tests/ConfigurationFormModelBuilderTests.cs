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

            var model = SUT.BuildFormModel().AvailableProviders.Items;
            int i = 0;
            for (; i < providerDO.VirtualFileProviderNames.Count; i++)
            {
                Assert.That(model[i], Is.EqualTo(providerDO.VirtualFileProviderNames[i]));
            }
            Assert.That(i, Is.GreaterThan(1));
        }

        [Test]
        public void should_set_value_by_active_provider()
        {
            var providerDO = Fixture.Create<AvailableVirtualFileProviderDO>();

            _configurationProvider.Setup(c => c.ReadToDO()).Returns(providerDO);

            var model = SUT.BuildFormModel().AvailableProviders;

            Assert.That(model.Value, Is.EqualTo(providerDO.CurrentProviderName));
        }
        
        [Test]
        public void should_find_selected_item()
        {
            var providerDO = Fixture.Create<AvailableVirtualFileProviderDO>();
            providerDO.CurrentProviderName = providerDO.VirtualFileProviderNames[1];

            _configurationProvider.Setup(c => c.ReadToDO()).Returns(providerDO);

            var model = SUT.BuildFormModel().AvailableProviders;

            var selectedItem = model.GetSelectedItem();
            Assert.That(selectedItem, Is.EqualTo(providerDO.VirtualFileProviderNames[1]));
        }
    }
}
