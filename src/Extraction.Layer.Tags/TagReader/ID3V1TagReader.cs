using System;
using System.IO;
using Extraction.Layer.Tags.DomainObjects;

namespace Extraction.Layer.Tags.TagReader
{
    public class ID3V1TagReader : IMp3TagReader
    {
        public int Order { get { return 4; } }

        public StreamTagContent ReadAllTagData(Stream stream)
        {
           
            return null;
        }

        public bool Supports(Stream stream)
        {
            var tag = new byte[3];

            stream.Seek(-128, SeekOrigin.End);
            stream.Read(tag, 0, 3);
            
            //                T               A               G
            return (tag[0] == 84 && tag[1] == 65 && tag[2] == 71);            
        }
    }
}
