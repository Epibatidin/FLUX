using Extraction.Interfaces;
using Extraction.Layer.Tags.DomainObjects;

namespace Extraction.Layer.Tags
{
    public class TagTreeByKeyAccessor : ISongByKeyAccessor
    {
        public ISong GetByKey(int key)
        {
            return new TagSong()
            {
                Id = key
            };
        }
    }
}
