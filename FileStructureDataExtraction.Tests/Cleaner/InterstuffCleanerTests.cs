using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.StringManipulation;
using FileStructureDataExtraction.Cleaner;
using FileStructureDataExtraction.Extraction;
using NUnit.Framework;

namespace FileStructureDataExtraction.Tests.Cleaner
{
    [TestFixture]
    public class InterstuffCleanerTests
    {
        private InternetStuffCleaner _cleaner;

        [SetUp]
        public void Setup()
        {
            _cleaner = new InternetStuffCleaner();
        }
        
        [TestCase("example.net")]
        [TestCase("www.example.de")]
        [TestCase("www.example.to")]
        [TestCase("www.example.net")]
        [TestCase("www.example.com")]
        [TestCase("http://www.example.com")]
        [TestCase("https://www.example.com?key=value&key=value")]
        public void should_remove_urls(string url)
        {
            var result = _cleaner.Filter(new PartedString(url));
            Assert.That(result.ToString(), Is.EqualTo(""));
        }

        
    }
}
