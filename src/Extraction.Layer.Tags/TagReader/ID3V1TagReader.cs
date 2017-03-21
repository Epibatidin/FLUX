using System;
using System.IO;
using Extraction.Layer.Tags.DomainObjects;
using Extraction.Layer.Tags.Interfaces;

namespace Extraction.Layer.Tags.TagReader
{
    public class ID3V1TagReader : IMp3TagReader
    {
        public int Order { get { return 4; } }

        public StreamTagContent ReadAllTagData(Stream stream)
        {
            throw new NotImplementedException();

            //    private void CreateV1Frames(Stream stream)
            //{
            //    // ID1 header am ende ... 
            //    stream.Seek(-125, SeekOrigin.End);
            //    // 30 zeichen title     => TIT2    
            //    // 30 zeichen artist    => TCOM
            //    // 30 zeichen album     => TALB    
            //    // 4 zeichen jahr       => TYER    
            //    // 30 zeichen commentar - kann auch den track enthalten (TRCK) - der letzte byte
            //    // 1 byte Genre 
            //    // == 125 

            //    foreach (var item in new[] { "TIT2", "TCOM", "TALB" })
            //    {
            //        AddFrame(item, ByteHelper.BytesToString(stream.Read(30)));
            //    }
            //    AddFrame("TYER", ByteHelper.BytesToString(stream.Read(4)));

            //    // comment - egal
            //    stream.Seek(28, SeekOrigin.Current);
            //    var track = stream.Read(2);
            //    if (track[0] == 0 && track[1] != 0)
            //    {
            //        // dann ist track[1] eine zahl;
            //        if (track[1] < 48)
            //        {
            //            AddFrame("TRCK", ((int)track[1]).ToString());
            //        }
            //    }
            //}

        }

        public bool Supports(Stream stream)
        {
            var tag = new byte[3];

            stream.Seek(-128, SeekOrigin.End);
            stream.Read(tag, 0, 3);

            //                T               A               G
            return (tag[0] == 84 && tag[1] == 65 && tag[2] == 71);
        }
    }
}
