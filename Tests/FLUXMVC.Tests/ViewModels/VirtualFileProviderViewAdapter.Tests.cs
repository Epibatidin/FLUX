using System.Linq;
using FLUXMVC.ViewModels;
using NUnit.Framework;
using TestHelpers;

namespace FLUXMVC.Tests.ViewModels
{
    [TestFixture]
    public class VirtualFileProviderViewAdapterTests
    {
        private VirtualFileProviderViewAdapter _adapter;

        [SetUp]
        public void Setup()
        {
            _adapter = new VirtualFileProviderViewAdapter(new StaticConfigurationLocator("VirtualFileAccessor"));
        }


        [Test]
        public void ShouldKnowAllAvailableProviders()
        {
            var provider = _adapter.Providers;

            Assert.That(provider.Count(), Is.EqualTo(3));
        }

        [Test]
        public void ShouldFindDefaultProvider()
        {
            var provider = _adapter.GetProvider(_adapter.DefaultProvider);

            Assert.That(provider, Is.Not.Null);
        }
    }
}
