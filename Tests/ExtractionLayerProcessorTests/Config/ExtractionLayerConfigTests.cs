using System.Configuration;
using ExtractionLayerProcessor.Config;
using NUnit.Framework;
using TestHelpers;

namespace ExtractionLayerProcessorTests.Config
{
    [TestFixture]
    public class ExtractionLayerConfigTests
    {
        private ExtractionLayerConfig _sectiongrp;

        [SetUp]
        public void Setup()
        {
            var locater = new StaticConfigurationLocator("Layer");
            var config = locater.Locate();
            _sectiongrp = config.GetSection("ExtractionLayer") as ExtractionLayerConfig;
        }

        [Test]
        public void should_parse_async()
        {
            Assert.That(_sectiongrp.ASync, Is.True);
        }

        [Test]
        public void should_find_both_layers()
        {
            var layers = _sectiongrp.LayerCollection;
            Assert.That(layers.Count, Is.EqualTo(3));
        }

        [Test]
        public void should_parse_isActive()
        {

            var layers = _sectiongrp.LayerCollection;

            Assert.That(layers.Item(0).IsActive, Is.True);
            Assert.That(layers.Item(1).IsActive, Is.False);
            Assert.That(layers.Item(2).IsActive, Is.False);
        }

         [Test]
        public void should_parse_layer_names()
        {
            var layers = _sectiongrp.LayerCollection;

            Assert.That(layers.Item(0).Key, Is.EqualTo("File"));
            Assert.That(layers.Item(1).Key, Is.EqualTo("Tag"));
        }

    }
}
