//using Extension.Test;
//using FLUX.DomainObjects;
//using FLUX.Interfaces.Web;
//using FLUX.Web.MVC.Controllers;
//using Moq;
//using NUnit.Framework;
//using Xunit;
//using Assert = NUnit.Framework.Assert;

//namespace FLUX.Web.MVC.Tests.Controller
//{
//    public class ConfigurationControllerTests : FixtureBase<ConfigurationController>
//    {
//        private Mock<IConfigurationFormModelBuilder> _selectionBuilder;

//        protected override void Customize()
//        {
//            _selectionBuilder = FreezeMock<IConfigurationFormModelBuilder>();
//        }

//        [Fact]
//        public void should_invoke_configuration_builder()
//        {
//            SUT.Index();

//            _selectionBuilder.Verify(c =>c.Build());            
//        }

//        //[Fact]
//        //public void should_return_index_view_with_form_model()
//        //{
//        //    var configurationFormModel = new ConfigurationFormModel();
//        //    _selectionBuilder.Setup(c => c.Build()).Returns(configurationFormModel);

//        //    var result = SUT.Index();

//        //    Assert.That(result.ViewData.Model, Is.SameAs(configurationFormModel));
//        //    Assert.That(result.ViewName, Is.EqualTo("Index"));
//        //}
//    }
//}