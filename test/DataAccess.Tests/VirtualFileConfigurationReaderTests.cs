//using DataAccess.Base;
//using DataAccess.Base.Config;
//using Facade.Configuration;
//using Extension.Test;
//using Moq;
//using NUnit.Framework;
//using Ploeh.AutoFixture;

//namespace DataAccess.Tests
//{
//    public class VirtualFileConfigurationReaderTests : FixtureBase<VirtualFileConfigurationReader>
//    {
//        private Mock<IVirtualFileAccessorSectionGroupProvider> _configProvider;
//        private VirtualFileAccessorSectionGroup _section;

//        [TestFixtureSetUp]
//        public void FixtureSetup()
//        {
//            var locator =
//                new StaticConfigurationLocator(@"F:\Develop\FLUX\ConfigFilesFor.Tests");

//            var config = locator.Locate("ConfigReaderTest");
//            _section = config.GetSectionGroup("VirtualFileAccessor") as VirtualFileAccessorSectionGroup;
//        }

//        protected override void Customize()
//        {
//            _configProvider = Fixture.Freeze<Mock<IVirtualFileAccessorSectionGroupProvider>>();
//            _configProvider.Setup(c => c.Configuration).Returns(_section);
//        }

//        [Test]
//        public void should_have_current_key_from_general()
//        {
//            var result = SUT.ReadToDO();

//            Assert.That(result.CurrentProviderName, Is.EqualTo("XMLSource"));
//        }

//        [Test]
//        public void should_build_list_of_available_provider_groups()
//        {
//            var result = SUT.ReadToDO();

//            Assert.That(result.ProviderNames[0].GroupName, Is.EqualTo("XML"));
//            Assert.That(result.ProviderNames[1].GroupName, Is.EqualTo("Directory"));
//        }


//        [Test]
//        public void should_build_list_of_available_providers()
//        {
//            var result = SUT.ReadToDO();

//            Assert.That(result.ProviderNames[0].VirtualFileProviderNames[0], Is.EqualTo("XMLSource"));
//            Assert.That(result.ProviderNames[1].VirtualFileProviderNames[0], Is.EqualTo("Normal"));
//            Assert.That(result.ProviderNames[1].VirtualFileProviderNames[1], Is.EqualTo("MOQ"));
//        }

//        [Test]
//        public void should_always_reset_iterator()
//        {
//            _section.Sources.MoveNext();

//            var result = SUT.ReadToDO();

//            Assert.That(result.ProviderNames.Count, Is.EqualTo(2));
//        }
//    }
//}
