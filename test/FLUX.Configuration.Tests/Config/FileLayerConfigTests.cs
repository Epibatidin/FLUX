using Extraction.Layer.File.Config;
using NUnit.Framework;

namespace FLUX.Configuration.Tests.Config
{
    public class FileLayerConfigTests : ConfigurationTestBase<FileLayerConfig>
    {
        public FileLayerConfigTests() : base(@"D:\Develop\FLUX\src\FLUX.Configuration\Files\Layer.json", "File")
        {
           
        }
        
        [Test]
        public void should_can_read_blacklist()
        {
            var blacklist = RetrieveFromConfig(c => c.BlackList);

            Assert.That(blacklist.Count, Is.GreaterThan(0));
        }

        [Test]
        public void should_can_read_whitelist()
        {
            var blacklist = RetrieveFromConfig(c => c.WhiteList);

            Assert.That(blacklist.Count, Is.GreaterThan(0));
        }
    }
}
