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
                // IDV4
                { "TALB", new TagMap() { Map = (r,b) => r.Album = b , Priority=1 } },
                { "TDAT", new TagMap() { Map = (r,b) => r.Year = int.Parse(b) , Priority=1 } },
                { "TIT2", new TagMap() { Map = (r,b) => r.SongName = b , Priority=1 } },
                { "TORY", new TagMap() { Map = (r,b) => r.Year = int.Parse(b) , Priority=2 } },
                { "TYER", new TagMap() { Map = (r,b) => r.Year = int.Parse(b) , Priority=3 } },
        };

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
