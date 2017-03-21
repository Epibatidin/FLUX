//using Extension.Test;
//using Extraction.Layer.Tags.TagReader;
//using NUnit.Framework;
//using System.IO;

//namespace Extraction.Tests.Layer.Tags.Integration
//{
//    public class ID3V3TagReaderTests : FixtureBase<ID3V3TagReader>
//    {
//        public Stream OpenFile()
//        {
//            var fileName = @"D:\FluxWorkBenchFiles\ComponentTests\Tags\";
//            fileName += @"IDV3.mp3";

//            var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

//            return fileStream;
//        }

//        [Test]
//        public void should_find_id3_Tag()
//        {
//            var stream = OpenFile();
            
//            var isId2 = SUT.Supports(stream);

//            Assert.That(isId2, Is.True);
//        }      

//        protected override ID3V3TagReader CreateSUT()
//        {
//            return new ID3V3TagReader();
//        }
//    }
//}
