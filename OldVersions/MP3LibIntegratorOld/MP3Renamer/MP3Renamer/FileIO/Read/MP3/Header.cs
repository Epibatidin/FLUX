using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MP3Renamer.Helper;
using System.IO;

namespace MP3Renamer.FileIO.Read.MP3
{
    public class Header
    { 
        List<int> FrameStarts = new List<int>()
        {
            3 ,     // ID3v2/file identifier  "ID3" 
            3 ,     // ID3v2 version           $03 00
            4 ,     // ID3v2 size             4 * %0xxxxx          
        };


        private Stream stream;
        private MP3Pattern pat;
        private List<Tuple<int, object>> Values;
        private List<DataFrame> Frames;

        public Header(MP3Pattern pattern)
        {
            pat = pattern;
            Frames = new List<DataFrame>();
        }

        

        private object Read(int frame, Func<byte[],object> func)
        {
            byte[] result = new byte[frame];

            stream.Read(result, 0, frame);

            if (func == null)
                return null;

            return func(result);
        }



        private Tuple<int, object> newTuple(int pos)
        {
            return new Tuple<int, object>(pos , Read(FrameStarts[pos], pat.GetTranslator(pos)));
        }

        private Dictionary<string, string> RelevantContent = new Dictionary<string, string>()
        {
            { "TYER" , "Year" }, 
            { "TIT2" , "Titel"}, 
            { "TRCK" , "Track"},
            { "TALB" , "Album"}
        };


        public long Read(Stream stream)
        {
            this.stream = stream;

            Values = new List<Tuple<int, object>>();
            
            int pos = 0;
                                  
            for (; pos < FrameStarts.Count; pos++)
            {
                Values.Add(newTuple(pos) );
            }

            int EndPos = (int)Values.Last().Item2; // size mit header => header = 10 groß
            
            do
            {
                var frame = new DataFrame();
                if (!frame.Extract(stream))
                    continue;
              
                if (RelevantContent.ContainsKey(frame.Tag))
                    Frames.Add(frame);
            }
            while (stream.Position <= EndPos);
            return stream.Position;
        }
    }
}