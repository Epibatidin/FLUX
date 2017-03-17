using Extension.Test;
using Extraction.Layer.Tags.TagReader;
using NUnit.Framework;
using System.IO;

namespace Extraction.Tests.Layer.Tags
{
    public class ID3TagReaderTests : FixtureBase<ID3V1TagReader>
    {
        protected override ID3V1TagReader CreateSUT()
        {
            return new ID3V1TagReader();
        }

        private Stream StringToStream(string content)
        {
            var bytes = new byte[content.Length];

            for (int i = 0; i < content.Length; i++)
            {
                bytes[i] = (byte)content[i];
            }
            var stream = new MemoryStream(bytes);

            return stream;
        }

        [Test]
        public void should_return_true_for_ID3()
        {

        }
    }
}
