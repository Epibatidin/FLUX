using System;
using System.IO;
using System.Linq;

namespace Extraction.Layer.Tags
{
    public interface IMp3TagVersionResolver
    {
    }

    public class Mp3TagVersionResolver
    {
        public IMp3TagReader[] _supportedReaders;

        public Mp3TagVersionResolver(IMp3TagReader[] supportedReaders)
        {
            _supportedReaders = supportedReaders.OrderBy(c => c.Order).ToArray();
        }

        public IMp3TagReader ResolverTagReader(Stream stream)
        {



            return null;
        }
    }

    public interface IMp3TagReader
    {
        int Order { get; }

        bool Supports(Stream stream);
    }

    public class ID3TagReader : IMp3TagReader
    {
        public int Order { get { return 0; } }

        public bool Supports(Stream stream)
        {
            var bytes = new byte[3];

            stream.Read(bytes, 0, 3);

            return true;
        }
    }
}