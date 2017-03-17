using Extension.Test;
using Extraction.Layer.Tags.Interfaces;
using Extraction.Layer.Tags.TagReader;
using Moq;
using NUnit.Framework;
using System.IO;

namespace Extraction.Tests.Layer.Tags.Integration
{
    public class ID3V2TagReaderTests : FixtureBase<ID3V2TagReader>
    {
        public Stream OpenFile()
        {
            var fileName = @"D:\FluxWorkBenchFiles\ComponentTests\Tags\";
            fileName += @"ID3V2.mp3";

            var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            return fileStream;
        }

        [Test]
        public void should_find_id3_Tag()
        {
            var stream = OpenFile();
            
            var isId2 = SUT.Supports(stream);

            Assert.That(isId2, Is.True);
        }


        [Test]
        public void should_extract_Frames_v2()
        {
            var stream = OpenFile();

            var isId2 = SUT.ReadAllTagData(stream);

            Assert.That(isId2.Frames.Count, Is.EqualTo(2));
        }


        protected override ID3V2TagReader CreateSUT()
        {
            var reader = new ID3V2TagReader();
            var mapper = new Mock<IFrameMapper>();
            mapper.Setup(c => c.IsSupported(It.IsAny<string>())).Returns(true);

            reader.FrameMapper = mapper.Object;
            return reader;
        }
    }
}
