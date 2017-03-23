using Extraction.Layer.Tags.DomainObjects;
using System.IO;
using Extraction.Layer.Tags.Interfaces;

namespace Extraction.Layer.Tags.TagReader
{
    public abstract class ID3TagReader : IMp3TagReader
    {
        private byte _subversion;

        public ID3TagReader(byte subversion)
        {
            _subversion = subversion;
        }

        public int Order { get { return _subversion; } }

        protected static byte[] read(Stream stream, int length)
        {
            var result = new byte[length];

            stream.Read(result, 0, length);

            return result;
        }

        public abstract StreamTagContent ReadAllTagData(Stream stream);


        protected bool ShouldAddFrame(Frame frame)
        {
            if (frame.FrameID == "TPOS")
                throw new System.Exception("Found a usecase");

            if (frame.FrameData == null) return false;
            frame.FrameData = frame.FrameData.Trim('\0', '-', ' ');


            if (frame.FrameData.Length == 0) return false;

            return true;
        }

        public bool Supports(Stream stream)
        {
            var tag = new byte[3];

            stream.Read(tag, 0, 3);

            if (!(tag[0] == 73 && tag[1] == 68 && tag[2] == 51)) return false;

            var version = new byte[2];
            stream.Read(version, 0, 2);

            return version[0] == _subversion;
        }
    }
}
