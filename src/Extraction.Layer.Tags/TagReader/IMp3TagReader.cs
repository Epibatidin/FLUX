using System.IO;
using Extraction.Layer.Tags.DomainObjects;

namespace Extraction.Layer.Tags
{
    public interface IMp3TagReader
    {
        int Order { get; }

        bool Supports(Stream stream);

        StreamTagContent ReadAllTagData(Stream stream);
    }
}