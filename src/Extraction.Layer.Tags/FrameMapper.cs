using Extraction.Interfaces;
using Extraction.Layer.Tags.Config;
using Extraction.Layer.Tags.DomainObjects;
using Extraction.Layer.Tags.Interfaces;
using System;
using System.Collections.Generic;

namespace Extraction.Layer.Tags
{
    public class FrameMapper : IFrameMapper
    { 
        private class TagMap
        {
            public int Priority { get; set; }
            public Action<TagSong, string> Map { get; set; }
        }


        private Dictionary<string, TagMap> _supported = 
            new Dictionary<string, TagMap>(StringComparer.OrdinalIgnoreCase)
        {
                  // IDV2
                { "TRK", new TagMap() { Map = (r,b) => r.TrackNr = ToIntOr0(b) , Priority=1 } },
                { "TT2", new TagMap() { Map = (r,b) => r.SongName = b , Priority=1 } },
                { "TP1", new TagMap() { Map = (r,b) => r.Artist = b , Priority=1 } },
                { "TAL", new TagMap() { Map = (r,b) => r.Album = b , Priority=1 } },
                { "TPA", new TagMap() { Map = (r,b) => r.CD = b , Priority=2 } },
                { "TYE", new TagMap() { Map = (r,b) => r.Year = ToIntOr0(b) , Priority=2 } },
                { "TLE", new TagMap() { Map = (r,b) => r.Misc.Add("TLE => " + b) , Priority=2 } },

                //{ "TORY", new TagMap() { Map = (r,b) => r.Year = ToIntOr0(b) , Priority=2 } },
                //{ "TYER", new TagMap() { Map = (r,b) => r.Year = ToIntOr0(b) , Priority=3 } },

                // IDV4
                { "TALB", new TagMap() { Map = (r,b) => r.Album = b , Priority=1 } },
                { "TDAT", new TagMap() { Map = (r,b) => r.Year =ToIntOr0(b) , Priority=1 } },
                { "TIT2", new TagMap() { Map = (r,b) => r.SongName = b , Priority=1 } },
                { "TORY", new TagMap() { Map = (r,b) => r.Year = ToIntOr0(b) , Priority=2 } },
                { "TYER", new TagMap() { Map = (r,b) => r.Year = ToIntOr0(b) , Priority=3 } },

               
        };

        private static int ToIntOr0(string value)
        {
            int result = 0;

            if (int.TryParse(value, out result))
                return result;
            return 0;
        }

        public FrameMapper(TagConfig config)
        {
            if (!config.IgnorePrivateData)
                _supported.Add("PRIV", null);
        }

        public bool IsSupported(string frameID)
        {
            // foo baz 
            // duales mapping ?! 
            // von frame in word von word in Property ? 


            return _supported.ContainsKey(frameID);
        }
        
    }
}
