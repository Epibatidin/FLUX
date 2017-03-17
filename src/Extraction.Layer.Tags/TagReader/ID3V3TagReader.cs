using System;
using System.IO;
using Extraction.Layer.Tags.DomainObjects;

namespace Extraction.Layer.Tags.TagReader
{
    public class ID3V3TagReader : ID3TagReader
    {
        public ID3V3TagReader() : base(3)
        {
        }

        public override StreamTagContent ReadAllTagData(Stream stream)
        {
            return null;
        }
    }
}
