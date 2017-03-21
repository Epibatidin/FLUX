using Extension.Test;
using Extraction.Layer.Tags.TagReader;
using NUnit.Framework;
using System.IO;

namespace Extraction.Tests.Layer.Tags.Integration
{
    public class ID3V4TagReaderTests : FixtureBase<ID3V4TagReader>
    {
        public Stream OpenFile()
        {
            var fileName = @"D:\FluxWorkBenchFiles\ComponentTests\Tags\";
            fileName += @"ID3V4.mp3";

            var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            return fileStream;
        }

        protected override ID3V4TagReader CreateSUT()
        {
            return new ID3V4TagReader();
        }

        [Test]
        public void should_support_id3v4()
        {
            var stream = OpenFile();

            var isId2 = SUT.Supports(stream);

            Assert.That(isId2, Is.True);
        }

        [Test]
        public void should_read_length()
        {
            var stream = OpenFile();

            var isId2 = SUT.ReadAllTagData(stream);

            Assert.That(isId2.DataStart, Is.EqualTo(4096));
        }

        [Test]
        public void should_extract_Frames_v4()
        {
            var stream = OpenFile();

            var isId2 = SUT.ReadAllTagData(stream);

            Assert.That(isId2.Frames.Count, Is.EqualTo(2));
        }

    }
}
