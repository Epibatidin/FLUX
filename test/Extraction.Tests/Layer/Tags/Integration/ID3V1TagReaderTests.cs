using Extension.Test;
using Extraction.Layer.Tags.TagReader;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Extraction.Tests.Layer.Tags.Integration
{
    public class ID3V1TagReaderTests : FixtureBase<ID3V1TagReader>
    {
        public Stream OpenFile()
        {
            var fileName = @"D:\FluxWorkBenchFiles\ComponentTests\Tags\";
            fileName += @"1.mp3";

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

      

        protected override ID3V1TagReader CreateSUT()
        {
            return new ID3V1TagReader();
        }
    }
}
