﻿using System;
using DataAccess.Base.Config;
using DataAccess.Interfaces;
using Facade.MVC;
using FLUX.Configuration.DependencyInjection;
using FLUX.Interfaces.Web;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;
using Moq;
using Xunit;
using Is = NUnit.Framework.Is;
using Assert = NUnit.Framework.Assert;

namespace FLUX.Configuration.Tests.DependencyInjection
{
    public class DependencyInstallerTests
    {
        private Mock<IConfigurationRoot> _configurationRoot;
        private IServiceCollection _container;
        private IServiceProvider _serviceprovider;

        public DependencyInstallerTests()
        {
            _container = new ServiceCollection();
            var installer = new DependencyInstaller();

            _configurationRoot = new Mock<IConfigurationRoot>();

            AddMock<IOptions<VirtualFileAccessorSectionGroup>>();
            AddMock<IHttpContextAccessor>();

            installer.Install(_container, _configurationRoot.Object);
            _serviceprovider = _container.BuildServiceProvider();
        }

        private void AddMock<TInterface>() where TInterface : class
        {
            _container.Add(new ServiceDescriptor(typeof(TInterface), new Mock<TInterface>().Object));

        }

        [Theory]
        [InlineData(typeof(IConfigurationFormProcessor))]
        [InlineData(typeof(IVirtualFileConfigurationReader))]
        //[TestCase(typeof(IVirtualFileAccessorSectionGroupProvider))]

        [InlineData(typeof(IPostbackHelper))]
        [InlineData(typeof(IModelBinderFacade))]
        public void should_can_resolve(Type componenType)
        {
            var result = _serviceprovider.GetService(componenType);

            Assert.That(result, Is.Not.Null);
        }
    }
}