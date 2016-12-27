using System;
using DataAccess.Interfaces;
using Extension.Test;
using Facade.MVC;
using FLUX.DomainObjects;
using FLUX.Interfaces.Web;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using FLUX.Interfaces;

namespace FLUX.Web.Logic.Tests
{
    public class ConfigurationFormModelBuilderTests : FixtureBase<ConfigurationFormProcessor>
    {
        private Mock<IVirtualFileConfigurationReader> _configurationProvider;
        private Mock<IModelBinderFacade> _modelBinder;
        private Mock<IPostbackHelper> _postbackHelper;

        protected override void Customize()
        {
            _configurationProvider = FreezeMock<IVirtualFileConfigurationReader>();
            _configurationProvider.Setup(c => c.ReadToDO()).Returns(new AvailableVirtualFileProviderDo());


            _modelBinder = FreezeMock<IModelBinderFacade>();
            _postbackHelper = FreezeMock<IPostbackHelper>();
        }


        protected override ConfigurationFormProcessor CreateSUT()
        {
            var httpContextAccessor = new Mock<IHttpContextAccessor>();

            var vitualFilePersistenceHelper = new Mock<IVirtualFilePeristentHelper>();

            return new ConfigurationFormProcessor(_configurationProvider.Object, _postbackHelper.Object,
                _modelBinder.Object,
                httpContextAccessor.Object,
                vitualFilePersistenceHelper.Object); 
        }

        [Test]
        public void should_return_new_formmodel()
        {
            var model = SUT.Build();
            Assert.That(model, Is.Not.Null);
            Assert.That(model, Is.TypeOf<ConfigurationFormModel>());
        }

        [Test]
        public void should_get_available_virtual_file_provider_from_configuration_provider()
        {
            var providerDO = Create<AvailableVirtualFileProviderDo>();
            _configurationProvider.Setup(c => c.ReadToDO()).Returns(providerDO);

            var model = SUT.Build();

            _configurationProvider.Verify(c => c.ReadToDO());
        }

        [Test]
        public void should_copy_available_provider_names_from_configuration_provider_result()
        {                               
            var providerDO = Create<AvailableVirtualFileProviderDo>();
            _configurationProvider.Setup(c => c.ReadToDO()).Returns(providerDO);

            var model = SUT.Build();

            Assert.That(model.VirtualFileProvider.CurrentProviderName, Is.EqualTo(providerDO.CurrentProviderName));
            Assert.That(model.VirtualFileProvider.ProviderNames, Is.EqualTo(providerDO.ProviderNames));
        }

        [Test]
        public void should_invoke_model_binder_if_is_postback()
        {
            var controller = new Mock<HttpRequest>();

            _postbackHelper.Setup(c => c.IsPostback(It.IsAny<HttpRequest>())).Returns(true);

            var bindingContext = new ModelBinderContext();
            var configurationFormModel = new ConfigurationFormModel();
            SUT.Update(configurationFormModel, controller.Object, c => bindingContext);

            _modelBinder.Verify(c => c.TryUpdateModel(configurationFormModel, bindingContext));
        }

    }
}
