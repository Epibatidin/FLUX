using DataAccess.Interfaces;
using Extension.Test;
using Facade.MVC;
using FLUX.DomainObjects;
using FLUX.Interfaces.Web;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace FLUX.Web.Logic.Tests
{
    public class ConfigurationFormModelBuilderTests : FixtureBase<ConfigurationFormModelBuilder>
    {
        private Mock<IVirtualFileConfigurationReader> _configurationProvider;
        private Mock<IModelBinderFacade> _modelBinder;
        private Mock<IPostbackHelper> _postbackHelper;

        protected override void Customize()
        {
            _configurationProvider = Fixture.Freeze<Mock<IVirtualFileConfigurationReader>>();

            _modelBinder = FreezeMock<IModelBinderFacade>();
            _postbackHelper = FreezeMock<IPostbackHelper>();

        }

        [Fact]
        public void should_return_new_formmodel()
        {
            var model = SUT.Build();
            Assert.That(model, Is.Not.Null);
            Assert.That(model, Is.TypeOf<ConfigurationFormModel>());
        }

        [Fact]
        public void should_get_available_virtual_file_provider_from_configuration_provider()
        {
            var providerDO = Fixture.Create<AvailableVirtualFileProviderDo>();
            _configurationProvider.Setup(c => c.ReadToDO()).Returns(providerDO);

            var model = SUT.Build();

            _configurationProvider.Verify(c => c.ReadToDO());
        }

        [Fact]
        public void should_copy_available_provider_names_from_configuration_provider_result()
        {                               
            var providerDO = Fixture.Create<AvailableVirtualFileProviderDo>();
            _configurationProvider.Setup(c => c.ReadToDO()).Returns(providerDO);

            var model = SUT.Build();

            Assert.That(model.VirtualFileProvider.CurrentProviderName, Is.EqualTo(providerDO.CurrentProviderName));
            Assert.That(model.VirtualFileProvider.ProviderNames, Is.EqualTo(providerDO.ProviderNames));
        }

        [Fact]
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
