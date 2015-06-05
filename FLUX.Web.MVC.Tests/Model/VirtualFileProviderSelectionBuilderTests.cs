using DataAccess.Base.Config;
using Extension.Test;
using FLUX.Web.MVC.Models;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace FLUX.Web.MVC.Tests.Model
{
    public class VirtualFileProviderSelectionBuilderTests : FixtureBase<ConfigurationFormModelBuilder>
    {
        private Mock<IVirtualFileAccessorSectionGroupProvider> _configProvider;

        protected override void Customize()
        { 
            _configProvider = Fixture.Freeze<Mock<IVirtualFileAccessorSectionGroupProvider>>();
        }

        [Test]
        public void should_request_all_providers_from_config()
        {
            //_configProvider.Setup(c => c.VirtualFileAccessorConfig).Returns();
        }

    }
}
