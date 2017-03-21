using Extraction.Layer.Tags.DomainObjects;

namespace Extraction.Layer.Tags.Interfaces
{
    public interface ITagSongFactory
    {
        TagSong Build(StreamTagContent content);
    }
}