using Extension.Test;
using Extraction.Layer.Tags.Interfaces;
using Extraction.Layer.Tags.TagReader;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Text;

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

        protected override ID3V2TagReader CreateSUT()
        {
            var mapper = new Mock<ITagSongFactory>();
            mapper.Setup(c => c.IsSupported(It.IsAny<string>())).Returns(true);
            var reader = new ID3V2TagReader(mapper.Object);
            return reader;
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

            Assert.That(isId2.Frames.Count, Is.EqualTo(9));
        }

        //[Test]
        //public void should_super_foo()
        //{
        //    var stream = OpenFile();
        //    stream.Seek(121, SeekOrigin.Begin);

        //    int length = 35;

        //    byte[] blubber = new byte[length];
        //    stream.Read(blubber, 0, length);

        //    blubber[6] = 1;

        //    var newStream = new MemoryStream(blubber);
            
        //    var frame = ID3V2TagReader.CreateV3Frame(newStream);

        //    var value = Encoding.GetEncoding("ISO-8859-1").GetString(blubber, 0, length);

        //    Assert.Fail();
        //}
        
        [Test]
        public void should_not_contain_string_terminator()
        {
            var stream = OpenFile();

            var isId2 = SUT.ReadAllTagData(stream);

            Assert.That(isId2.Frames[8].FrameData.Length, Is.EqualTo(17));
        }



    }
}
