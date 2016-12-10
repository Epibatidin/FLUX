
using DataStructure.Tree;
using Extension.Test;
using Extraction.DomainObjects.StringManipulation;
using Extraction.Layer.File;
using Extraction.Layer.File.FullTreeOperators;
using NUnit.Framework;

namespace Extraction.Tests.Layer.File.FullTreeOperators
{
    public class DropRedundantExtractedInformationTreeOperatorTests : FixtureBase<DropRedundantExtractedInformationTreeOperator>
    {
        protected override DropRedundantExtractedInformationTreeOperator CreateSUT()
        {
            return new DropRedundantExtractedInformationTreeOperator();
        }

        [Test]
        public void should_drop_redundant_information()
        {
            var artist = new PartedString("Ashbury Heights").Split();
            var album = new PartedString("ashbury heights - angora overdrive").Split();
            
            SUT.RemoveLongestMatch(album,artist);

            Assert.That(album.ToString(), Is.EqualTo("angora overdrive"));
        }
    }
}
