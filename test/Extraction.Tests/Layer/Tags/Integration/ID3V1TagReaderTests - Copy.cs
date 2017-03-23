using Extension.Test;
using Extraction.Layer.Tags.TagReader;
using NUnit.Framework;
using System.IO;

namespace Extraction.Tests.Layer.Tags.Integration
{
    public class ID3V3TagReaderTests : FixtureBase<ID3V3TagReader>
    {
        public Stream OpenFile()
        {
            var fileName = @"D:\FluxWorkBenchFiles\ComponentTests\Tags\";
            fileName += @"ID3V3.mp3";

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
        public void should_Read_all_frames()
        {
            var stream = OpenFile();

            var isId2 = SUT.ReadAllTagData(stream);

            Assert.That(isId2.Frames.Count, Is.GreaterThan(4));
        }

        protected override ID3V3TagReader CreateSUT()
        {
            return new ID3V3TagReader();
        }
    }
}
