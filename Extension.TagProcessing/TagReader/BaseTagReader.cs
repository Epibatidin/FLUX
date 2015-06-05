using System.Collections.Generic;
using System.IO;

namespace Extension.TagProcessing.TagReader
{
    public class BaseTagReader
    {
        protected Stream _source;
        private bool _ignorePrivateTags;

        private List<Frame> _frames;

        protected void AddFrame(string ID, string Value)
        {            
            AddFrame(new Frame(ID)
            {
                FrameData = Value
            });
        }

        protected void AddFrame(Frame F)
        {
            if (F == null) return;
            if (_ignorePrivateTags && F.FrameID == "PRIV") return;
            if (string.IsNullOrEmpty(F.FrameData)) return;
            _frames.Add(F);
        }


        public void SetStream(Stream source)
        {
            _frames = new List<Frame>();
            _source = source;
        }

        public MP3Data ReadFrame(bool ignorePrivateTags)
        {
            _ignorePrivateTags = ignorePrivateTags;
            var data = internalReadFrame();
            if (data != null)
            {
                data.Frames = _frames;
            }
            return data;
        }

        protected virtual MP3Data internalReadFrame()
        {
            return null;
        }

    }
}
