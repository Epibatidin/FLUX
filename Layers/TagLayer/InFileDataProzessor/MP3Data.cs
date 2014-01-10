using System.Collections.Generic;
using FrameHandler;

namespace InFileDataProzessor
{
    public class MP3Data
    {
        public long DataStart { get; set; }
        public long DataEnd { get; set; }

        public List<Frame> Frames { get; set; } 
    }
}
