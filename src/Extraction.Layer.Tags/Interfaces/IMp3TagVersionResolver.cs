using System.IO;

namespace Extraction.Layer.Tags.Interfaces
{
    public interface IMp3TagVersionResolver
    {
        IMp3TagReader ResolverTagReader(Stream stream);
    }
}
