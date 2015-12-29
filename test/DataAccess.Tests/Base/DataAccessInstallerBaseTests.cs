using Extension.Test;
using Moq;
using Xunit;
using DataAccess.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;
using Facade.Configuration;
using Microsoft.Extensions.Configuration;
using Assert = NUnit.Framework.Assert;
using Is = NUnit.Framework.Is;

namespace DataAccess.Tests.Base
{
    public class VirtualFileRootConfigurationDummy : IVirtualFileRootConfiguration
    {
        public string ID { get; set; }
        public IEnumerable<string> Keys { get; }
    }


    public class DataAccessInstallerImpl : DataAccessInstallerBase<VirtualFileRootConfigurationDummy>
    {
        public override void RegisterServices(IServiceCollection serviceCollection)
        {
        }
    }


    public class DataAccessInstallerBaseTests : FixtureBase<DataAccessInstallerImpl>
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
            
            Assert.Throws<NotImplementedException>(() => SUT.Install(configuration.Object, "sectionName", service.Object));
        }

    }
}
