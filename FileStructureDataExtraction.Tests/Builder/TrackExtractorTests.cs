using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileStructureDataExtraction.Extraction;
using NUnit.Framework;

namespace FileStructureDataExtraction.Tests.Builder
{
    [TestFixture]
    public class TrackExtractorTests
    {
        private TrackExtractor _extractor;

        [SetUp]
        public void SetUp()
        {
            _extractor = new TrackExtractor(null);
        }

        [Test]
        public void should_be_allmighty()
        {
            //Assert.Fail();
        }

    }
}
