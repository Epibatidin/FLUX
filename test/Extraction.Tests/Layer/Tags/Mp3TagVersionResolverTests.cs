using Extension.Test;
using Extraction.Layer.Tags;
using Extraction.Layer.Tags.Interfaces;
using Moq;
using NUnit.Framework;
using System.IO;

namespace Extraction.Tests.Layer.Tags
{
    public class Mp3TagVersionResolverTests : FixtureBase<Mp3TagVersionResolver>
    {
        private Mock<IMp3TagReader> _firstReader;
        private Mock<IMp3TagReader> _secondReader;

        protected override void Customize()
        {
            _firstReader = FreezeMock<IMp3TagReader>();
            _firstReader.Setup(c => c.Order).Returns(0);
            _secondReader = FreezeMock<IMp3TagReader>();
            _secondReader.Setup(c => c.Order).Returns(1);
        }
        
        protected override Mp3TagVersionResolver CreateSUT()
        {
            var resolver =  new Mp3TagVersionResolver();
            resolver.SetReader(new[] { _firstReader.Object, _secondReader.Object });
            return resolver;
        }

        [Test]
        public void should_reorder_the_tag_readers()
        {
            var sequence = new MockSequence();

            _firstReader.Setup(c => c.Order).Returns(2);
            _secondReader.Setup(c => c.Order).Returns(1);

            _secondReader.InSequence(sequence).Setup(c => c.Supports(It.IsAny<Stream>())).Verifiable();
            _firstReader.InSequence(sequence).Setup(c => c.Supports(It.IsAny<Stream>())).Verifiable();


            SUT.SetReader(new[] { _firstReader.Object, _secondReader.Object });
            var stream = new MemoryStream();

            var result = SUT.ResolverTagReader(stream);

            
            _firstReader.Verify();
            _secondReader.Verify();
        }


        [Test]
        public void should_ask_all_tag_readers_for_support()
        {
            var stream = new MemoryStream();

            var result = SUT.ResolverTagReader(stream);

            _firstReader.Verify(c => c.Supports(stream));
            _secondReader.Verify(c => c.Supports(stream));
        }

        [Test]
        public void should_return_null_if_not_supported()
        {
            _firstReader.Setup(c => c.Supports(It.IsAny<Stream>())).Returns(false);
            _secondReader.Setup(c => c.Supports(It.IsAny<Stream>())).Returns(false);

            var result = SUT.ResolverTagReader(new MemoryStream());

            Assert.That(result, Is.Null);
        }

        [Test]
        public void should_return_first_match()
        {
            _firstReader.Setup(c => c.Supports(It.IsAny<Stream>())).Returns(true);
            _secondReader.Setup(c => c.Supports(It.IsAny<Stream>())).Returns(true);

            var result = SUT.ResolverTagReader(new MemoryStream());

            Assert.That(result, Is.SameAs(_firstReader.Object));
        }

    }
}
