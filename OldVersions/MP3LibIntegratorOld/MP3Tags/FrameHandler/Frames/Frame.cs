using System;
using System.IO;
using Helper;

namespace FrameHandler.Frames
{
    //[Tag(4)][Size(4)][Flags(2)][Content(Size)]
    public class Frame
    {
        public string FrameID { get; private set; } 
        public string FrameData { get; set; }
        public byte[] flags { get; private set; }

        public void SetFlags(byte[] Flags)
        {
            flags = Flags;
        }

        public int DataSize;
        
        public Frame(string _frameID)
        {
            FrameID = _frameID;
        }       
    }
}
