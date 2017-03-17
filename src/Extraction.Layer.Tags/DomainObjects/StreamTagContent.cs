using System.Collections.Generic;

namespace Extraction.Layer.Tags.DomainObjects
{
    public class StreamTagContent
    {
        public long DataStart { get; set; }
        public long DataEnd { get; set; }

        public List<Frame> Frames { get; set; }
    }
}
