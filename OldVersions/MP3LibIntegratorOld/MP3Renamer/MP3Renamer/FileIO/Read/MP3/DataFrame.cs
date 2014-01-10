using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using MP3Renamer.Helper;

namespace MP3Renamer.FileIO.Read.MP3
{
    //                   unused
    //[Tag(4)][Size(4)][Flags(2)][Content(Size)]
    public class DataFrame
    {

        public string Tag { get; private set; }
        public string Content { get; private set; }

        public bool Extract(Stream stream)
        {
            // Header  = text;
            // size = int 
            // flags = encoding 
            // der rest bis zum nächsten header => content

            // die ersten 4 byte sagen was für Content informationen folgen werden 
            Tag = ByteHelper.ByteArrayToString(StreamHelper.Read(stream, 4));
            // die nächsten 4 Byte sind die größe des Contents
            int contentSize = ByteHelper.calculateFrameSizeWithSevenBits(StreamHelper.Read(stream, 4));

            // die Flags enthalten keine Informationen die ich brauch
            stream.Seek(2, SeekOrigin.Current);
            if (contentSize == 0)
                return false ;
            // hier steht die Info drin 
            Content = ByteHelper.ByteArrayToString(StreamHelper.Read(stream, contentSize));
            return true;
        }
    }
}