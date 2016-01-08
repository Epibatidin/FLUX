using Extraction.Layer.File.Config;
using Xunit;
using Assert = NUnit.Framework.Assert;
using Is = NUnit.Framework.Is;

namespace FLUX.Configuration.Tests.Config
{
    public class FileLayerConfigTests : ConfigurationTestBase<FileLayerConfig>
    {
        public FileLayerConfigTests() : base(@"D:\Develop\FLUX\src\FLUX.Configuration\Files\Layer.json", "File")
        {
           
        }
        
        [Fact]
        public void should_can_read_blacklist()
        {
            var blacklist = RetrieveFromConfig(c => c.BlackList);

            Assert.That(blacklist.Count, Is.GreaterThan(0));
        }

        [Fact]
        public void should_can_read_whitelist()
        {
            var blacklist = RetrieveFromConfig(c => c.WhiteList);

            Assert.That(blacklist.Count, Is.GreaterThan(0));
        }
    }
}
