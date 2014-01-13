using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using ConfigurationExtensions;
using ConfigurationExtensions.Interfaces;
using Moq;
using NUnit.Framework;
using TestHelpers;

namespace ConfigurationExtensionsTests
{
    [TestFixture]
    public class SectionGroupSingletonHelperTests
    {
        private SectionGroupSingletonHelper<TestGroup> _helper;
        private Mock<IConfigurationLocator> _locator;
        private Configuration _config;

        [SetUp]
        public void Setup()
        {
            _helper = new SectionGroupSingletonHelper<TestGroup>("TestGroup");

            var locator = new StaticConfigurationLocator("SectionGroupSingletonHelper");
            _config = locator.Locate();

            _locator = new Mock<IConfigurationLocator>();
            _locator.Setup(c => c.Locate()).Returns(_config);
        }

        [Test]
        public void should_locate_section_if_null()
        {
            //var result = _helper.Get(_locator.Object.Locate);


            //Assert.That(result, Is.SameAs(_config));
        }


    }
}
