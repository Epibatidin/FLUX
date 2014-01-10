using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameHandler.ContainerDefs;
using System.IO;
using FrameHandler.Frames;

namespace FrameHandler
{
    public class Container
    {
        protected Stream SourceStream;

        public List<Frame> Frames { get; protected set; }
        public long DataStart { get; protected set; }
        public long DataEnd { get; protected set; }


        public void SetSourceStream(Stream stream)
        {
            SourceStream = stream;
            Frames = new List<Frame>();
            InitStream();
        }

        protected virtual void InitStream()
        {
            DataStart = 0;
            DataEnd = SourceStream.Length;
        }

        public virtual void ReadFrameCollection()
        {            
        }

        public void CopyTo(string Target)
        {
            FileStream targetStream = new FileStream(Target, FileMode.Create, FileAccess.Write);
            InternalWrite(targetStream);

        }

        protected virtual void InternalWrite(Stream Target)
        {

        }
    }
}
