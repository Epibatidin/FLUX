using Extension.Test;
using Extraction.Layer.Tags;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Extraction.Tests.Layer.Tags
{
    public class ID3TagReaderTests : FixtureBase<ID3TagReader>
    {
        protected override ID3TagReader CreateSUT()
        {
            return new ID3TagReader();
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
    }
}
