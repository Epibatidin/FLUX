
namespace Extraction.Layer.Tags.DomainObjects
{
    //[Tag(4)][Size(4)][Flags(2)][Content(Size)]
    public class Frame
    {
        public string FrameID { get; private set; }
        public string FrameData { get; set; }
        public byte[] Flags { get; set; }
        public int DataSize { get; set; }

        public Frame(string _frameID)
        {
            FrameID = _frameID.ToUpper();
        }
    }
}
