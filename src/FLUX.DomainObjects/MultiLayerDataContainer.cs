using System.Collections.Generic;
using Extension.IEnumerable;
using Extraction.Interfaces;

namespace FLUX.DomainObjects
{
    public class MultiLayerDataContainer
    {
        private readonly string[] _parts;
        public int Id { get; set; }

        public MultiLayerDataContainer(string[] parts)
        {
            _parts = parts;
            Data = new Dictionary<string, List<string>>();
        }

        public Dictionary<string, List<string>> Data;

        public MultiLayerDataContainer SetOriginalValue(int depth)
        {
            if (_parts.Length < depth)
                OriginalValue = _parts[depth];

            return this;
        }

        public string OriginalValue { get; private set; }
        
        public void AddSong(ISong song)
        {
            if (song == null) return;

            AddValue("Artist", song.Artist);
            AddValue("Year", song.Year);
            AddValue("Album", song.Album);
            AddValue("CD", song.CD);
            AddValue("Track", song.TrackNr);
            AddValue("Title", song.SongName);
        }

        public string GetGroupingKeyByDepth(int depth)
        {
            var key = GetKeyByDepth(depth);
            if (key == null) return null;

            if (!Data.ContainsKey(key))
                return null;

            return string.Join("-", Data[key]);
        }

        private string GetKeyByDepth(int depth)
        {
            switch (depth)
            {
                case 0: return "Artist";
                case 1: return "Album";
                case 2: return "CD";
                case 3: return "Title";
            }
            return null;
        }


        private void AddValue(string key, int value)
        {
            if (value == 0) return;
            AddValue(key, value.ToString());
        }

        private void AddValue(string key, string value)
        {
            if(string.IsNullOrEmpty(value)) return;
            List<string> values = Data.GetOrCreate(key);
            values.Add(value);
        }
    }
}