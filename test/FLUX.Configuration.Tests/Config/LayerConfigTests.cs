using Extraction.Base.Config;
using NUnit.Framework;

namespace FLUX.Configuration.Tests.Config
{
    public class LayerConfigTests : ConfigurationTestBase<ExtractionLayerConfig>
    {
        public LayerConfigTests() : base(@"D:\Develop\FLUX\src\FLUX.Configuration\Files\Layer.json", null)
        {

        }
        
        [Test]
        public void should_can_read_config_file()
        {
            var build = RetrieveFromConfig<ExtractionLayerConfig>();

            Assert.That(build.Value, Is.Not.Null);
        }

        [Test]
        public void should_not_be_async()
        {
            var bound = RetrieveFromConfig(c => c.ASync);

            Assert.That(bound, Is.False);
        }

        [Test]
        public void should_can_read_layer_items()
        {
            var bound = RetrieveFromConfig(c => c.Layers);
                
            Assert.That(bound.Length, Is.EqualTo(2));
        }


        [Test]
        public void should_not_be_empty_item()
        {
            var bound = RetrieveFromConfig(c => c.Layers[0]);

            Assert.That(bound.Type, Is.Not.Empty);
            Assert.That(bound.Active, Is.True);
            Assert.That(bound.ExtractorType, Is.Not.Empty);
        }
    }
}