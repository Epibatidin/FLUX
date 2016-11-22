using Extraction.DomainObjects.StringManipulation;
using Extraction.Layer.File.SimpleOperators;
using NUnit.Framework;

namespace ExtractionLayer.Tests.Files.SimpleOperators
{
    [TestFixture]
    public class DropNonWordPhraseOperationTests
    {
        private DropNonWordPhraseOperation SUT;

        [SetUp]
        public void Setup()
        {
            SUT = new DropNonWordPhraseOperation();
        }


        [Test]
        public void should_not_drop_numbers_with_dots()
        {
            var partedString = new PartedString("07.seeing you pray");
            SUT.Operate(partedString);

            Assert.That(partedString.ToString(), Is.EqualTo("07.seeing you pray"));
        }       
    }
}
