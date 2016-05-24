using DataAccess.Base.Config;
using Xunit;
using Assert = NUnit.Framework.Assert;
using Is = NUnit.Framework.Is;

namespace FLUX.Configuration.Tests.Config
{
    public class VirtualFileAccessorSectionGroupTests : ConfigurationTestBase<VirtualFileAccessorSectionGroup>
    {
        public VirtualFileAccessorSectionGroupTests() : base(@"D:\Develop\FLUX\src\FLUX.Configuration\Files\VirtualFileProvier.json", null)
        {         
        }
        
        [Fact]
        public void should_can_read_config_file()
        {
            var build = RetrieveFromConfig<VirtualFileAccessorSectionGroup>();

            Assert.That(build.Value, Is.Not.Null);
        }

        [Fact]
        public void should_can_bind_debug_config()
        {
            var debug = RetrieveFromConfig(r => r.Debug);

            Assert.That(debug.RootNames, Is.Not.Null);

            Assert.That(debug.SubRootPos, Is.Not.Null);
        }

        [Fact]
        public void should_can_bind_general_config()
        {
            var debug = RetrieveFromConfig(r => r.General);

            Assert.That(debug.Active, Is.Not.Empty);
        }

        [Fact]
        public void should_can_bind_sources_elements()
        {
            var debug = RetrieveFromConfig(r => r.Sources);

            Assert.That(debug.Length, Is.EqualTo(2));
        }

        [Fact]
        public void should_can_bind_source_values()
        {
            var debug = RetrieveFromConfig(r => r.Sources);

            var first = debug[0];

            Assert.That(first.Type, Is.Not.Empty);
            Assert.That(first.SetionName, Is.Not.Empty);
        }
    }
}
