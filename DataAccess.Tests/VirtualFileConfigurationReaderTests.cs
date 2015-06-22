using DataAccess.Base;
using DataAccess.Base.Config;
using Extension.Configuration;
using Extension.Test;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace DataAccess.Tests
{
    public class VirtualFileConfigurationReaderTests : FixtureBase<VirtualFileConfigurationReader>
    {
        private Mock<IVirtualFileAccessorSectionGroupProvider> _configProvider;
        private VirtualFileAccessorSectionGroup _section;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            var locator =
                new StaticConfigurationLocator(@"E:\Develop\FLUX\ConfigFilesFor.Tests");

            var config = locator.Locate("ConfigReaderTest");
            _section = config.GetSectionGroup("VirtualFileAccessor") as VirtualFileAccessorSectionGroup;
        }

        protected override void Customize()
        {
            _configProvider = Fixture.Freeze<Mock<IVirtualFileAccessorSectionGroupProvider>>();
            _configProvider.Setup(c => c.Configuration).Returns(_section);
        }

        [Test]
        public void should_have_current_key_from_general()
        {
            var result = SUT.ReadToDO();

            Assert.That(result.CurrentProviderName, Is.EqualTo("XMLSource"));
        }

        [Test]
        public void should_build_list_of_available_providers()
        {
            var result = SUT.ReadToDO();

            Assert.That(result.VirtualFileProviderNames[0], Is.EqualTo("XML"));
            Assert.That(result.VirtualFileProviderNames[1], Is.EqualTo("Directory"));
        }

        [Test]
        public void should_always_reset_iterator()
        {
            _section.Sources.MoveNext();

            var result = SUT.ReadToDO();

            Assert.That(result.VirtualFileProviderNames.Count, Is.EqualTo(2));
        }
    }
}
