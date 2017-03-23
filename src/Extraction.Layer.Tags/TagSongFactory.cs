using Extraction.Interfaces;
using Extraction.Layer.Tags.Config;
using Extraction.Layer.Tags.DomainObjects;
using Extraction.Layer.Tags.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Extraction.Layer.Tags
{
    public class TagSongFactory : ITagSongFactory
    { 
        private class TagMap
        {
            public int Relevance { get; set; }
            public Action<TagSong, string> Map { get; set; }
        }

        private Dictionary<string, TagMap> _supported = 
            new Dictionary<string, TagMap>(StringComparer.OrdinalIgnoreCase)
        {
                // ID3V1
            { "TP1", new TagMap() { Map = (r,b) => r.Artist = b , Relevance=1 } },
            { "TYE", new TagMap() { Map = (r,b) => r.Year = ToIntOr0(b) , Relevance=2 } },
            { "TAL", new TagMap() { Map = (r,b) => r.Album = b , Relevance=1 } },
            { "TPA", new TagMap() { Map = (r,b) => r.CD = b , Relevance=2 } },
            { "TRK", new TagMap() { Map = (r,b) => r.TrackNr = ForTrack(b) , Relevance=1 } },
            { "TT2", new TagMap() { Map = (r,b) => r.SongName = b , Relevance=1 } }, 
            { "TLE", new TagMap() { Map = (r,b) => r.Misc.Add("TLE => " + b) , Relevance=2 } },

            // ID3V2 - ID3V4
            { "TCOM", new TagMap() { Map = (r,b) => r.Artist = b , Relevance=1 } },
            { "TDAT", new TagMap() { Map = (r,b) => r.Year =ToIntOr0(b) , Relevance=1 } },
            { "TORY", new TagMap() { Map = (r,b) => r.Year = ToIntOr0(b) , Relevance=2 } },
            { "TYER", new TagMap() { Map = (r,b) => r.Year = ToIntOr0(b) , Relevance=3 } },
            { "TALB", new TagMap() { Map = (r,b) => r.Album = b , Relevance=1 } },                  
            { "TPOS", new TagMap() { Map = (r,b) => r.CD = b , Relevance=1 } },            
            { "TRCK", new TagMap() { Map = (r,b) => r.TrackNr = ForTrack(b) , Relevance=1 } },
            { "TIT2", new TagMap() { Map = (r,b) => r.SongName = b , Relevance=1 } },


               
        };

        private static int ToIntOr0(string value)
        {
            int result = 0;

            if (int.TryParse(value, out result))
                return result;
            return 0;
        }

        private static int ForTrack(string value)
        {
            string forIntConvertion = value;
            var parts = value.Split('/');
            if(parts.Length == 2)
                forIntConvertion = parts[0];
            
            int result = 0;
            if (int.TryParse(forIntConvertion, out result))
                return result;
            return 0;
        }

        public TagSongFactory(TagConfig config)
        {
            //if (!config.IgnorePrivateData)
            //    _supported.Add("PRIV", null);
        }
        
        public TagSong Build(StreamTagContent content)
        {
            var song = new TagSong();
            
            var listOfSupportedFrameMaps = new List<Tuple<TagMap, string>>();
            foreach (var frame in content.Frames)
            {
                TagMap map = null;
                if (!_supported.TryGetValue(frame.FrameID, out map)) continue;
                listOfSupportedFrameMaps.Add(Tuple.Create(map, frame.FrameData));
            }

            foreach (var map in listOfSupportedFrameMaps.OrderBy(c => c.Item1.Relevance))
            {
                map.Item1.Map(song, map.Item2);
            }
            return song;
        }

    }
}
