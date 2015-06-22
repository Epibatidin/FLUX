using System;
using Castle.Windsor;
using DataAccess.Base.Config;
using DataAccess.Interfaces;
using FLUX.Configuration.Windsor;
using FLUX.Interfaces.Web;
using NUnit.Framework;

namespace FLUX.Configuration.Tests.Windsor
{
    [TestFixture]
    public class WindsorInstallerTests
    {
        private WindsorContainer _container;

        [TestFixtureSetUp]
        public void Setup()
        {
            _container = new WindsorContainer();

            _container.Install(new WindsorInstaller(), new TheContainerInstallerWithMocks());
        }

        [TestCase(typeof(IConfigurationFormModelBuilder))]
        [TestCase(typeof(IVirtualFileConfigurationReader))]
        [TestCase(typeof(IVirtualFileAccessorSectionGroupProvider))]
        public void should_can_resolve(Type componenType)
        {
            _container.Resolve(componenType);
        }
    }
}