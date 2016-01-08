using Extension.Test;
using Facade.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace DynamicLoading.Tests
{
    public class VirtualFileRootConfigurationDummy : ISectionNameHolder
    {
        public string SectionName { get; set; }
    }


    public class DynamicExtensionInstallerImpl : DynamicExtensionInstallerBase<VirtualFileRootConfigurationDummy>
    {
        public IServiceCollection Service { get; private set; }

        public override void RegisterServices(IServiceCollection serviceCollection)
        {
            Service = serviceCollection;
        }
    }


    public class DynamicExtensionInstallerBaseTests : FixtureBase<DynamicExtensionInstallerImpl>
    {
        private Mock<IConfigurationBinderFacade> _configurationBinder;

        protected override void Customize()
        {
            _configurationBinder = FreezeMock<IConfigurationBinderFacade>();
        }

        [Fact]
        public void should_build_configuration_from_options()
        {
            var configuration = new Mock<IConfiguration>();

            var service = new Mock<IServiceCollection>();

            SUT.Install(configuration.Object, "sectionName", service.Object);
           
            _configurationBinder.Verify(c => c.Bind<VirtualFileRootConfigurationDummy>(configuration.Object, "sectionName"));
        }

        [Fact]
        public void should_call_register_services()
        {
            var configuration = new Mock<IConfiguration>();
            var obj = new VirtualFileRootConfigurationDummy();

            _configurationBinder.Setup(
                c => c.Bind<VirtualFileRootConfigurationDummy>(It.IsAny<IConfiguration>(), It.IsAny<string>()))
                .Returns(obj);
            
            var service = new Mock<IServiceCollection>();

            SUT.Install(configuration.Object, "sectionName", service.Object);

            Assert.That(SUT.Service, Is.SameAs(service.Object));
        }
    }
}
