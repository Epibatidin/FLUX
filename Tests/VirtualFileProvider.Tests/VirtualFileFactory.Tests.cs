using System.Linq;
using NUnit.Framework;
using TestHelpers;
using VirtualFileProvider.Config;

namespace VirtualFileProvider.Tests
{
    [TestFixture]
    public class VirtualFileFactoryTests
    {
        private VirtualFileAccessorSectionGroup _vfa;
        
        [SetUp]
        public void Setup()
        {
            _vfa = VirtualFileAccessorSectionGroup.Get(new StaticConfigurationLocator("VirtualFileAccessor"));
        }

        [Test]
        public void ShouldFindSourceByName()
        {
            var vf = new VirtualFileFactory(_vfa);

            var provider = vf.GetProvider("Normal");

            Assert.That(provider, Is.Not.Null);
        }

        [Test]
        public void ShouldFindAllSourceNames()
        {
            var vf = new VirtualFileFactory(_vfa);
            
            Assert.That(vf.AvailableProviders.Count(), Is.EqualTo(3));
        }



    }
}
