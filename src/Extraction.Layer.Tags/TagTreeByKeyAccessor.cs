using Extraction.Interfaces;
using System.Collections.Generic;

namespace Extraction.Layer.Tags
{
    public class TagTreeByKeyAccessor : ISongByKeyAccessor
    {
        private Dictionary<int, ISong> _dict;

        public TagTreeByKeyAccessor()
        {
            _dict = new Dictionary<int, ISong>();
        }
        

        public void Add(ISong song)
        {
            _dict.Add(song.Id, song);

        }



        public ISong GetByKey(int key)
        {
            return _dict[key];
        }
    }
}
