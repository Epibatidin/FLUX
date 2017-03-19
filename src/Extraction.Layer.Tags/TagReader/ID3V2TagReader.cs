using Extraction.Layer.Tags.DomainObjects;
using Extraction.Layer.Tags.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using Extraction.Layer.Tags.Interfaces;
using System.Text;

namespace Extraction.Layer.Tags.TagReader
{
    // http://id3.org/id3v2-00

    public class ID3V2TagReader : ID3TagReader
    {
        public ID3V2TagReader() : base(2)
        {
        }

        public override StreamTagContent ReadAllTagData(Stream stream)
        {
            var tagData = new StreamTagContent();
            tagData.DataStart = EvaluateBeginOfData(stream);
            tagData.Frames = new List<Frame>();
            stream.Seek(10, SeekOrigin.Begin);
            int i = 0;
            while (stream.Position < tagData.DataStart)
            {
                var frame = CreateV3Frame(stream);
                if (frame == null) break;
                if (string.IsNullOrEmpty(frame.FrameData)) continue;
                i++;

                tagData.Frames.Add(frame);
            }
            return tagData;
        }


        private long EvaluateBeginOfData(Stream stream)
        {
            stream.Seek(5, SeekOrigin.Begin);

            var flags = read(stream, 1)[0];
            bool unsynchronisation = ByteHelper.GetBit(flags, 7);
            if(unsynchronisation)
                throw new NotSupportedException("Dont know what this flag means");

            bool compression = ByteHelper.GetBit(flags, 6);

            var size = new byte[4];
            stream.Read(size, 0, 4);
            var length = ByteHelper.GetIntFrom7SignificantBitsPerByte(size) + 10;
            
            return length;
        }

        private static string readTextFrame(Stream stream, int dataSize)
        {
            var byteContent = read(stream,dataSize);

            int begin = 0;
            int length = dataSize;
            var encoder = Encoding.GetEncoding("ISO-8859-1");
            switch (byteContent[0])
            {
                case 0:
                    begin = 1;
                    length--;
                    break;
                case 1:
                    begin = 1;
                    length--;
                    encoder = Encoding.GetEncoding("ucs-2");                           
                    break;
            }
            byte nul = 0;
            for (int i = byteContent.Length - 1; i >= 0; i--)
            {
                if (byteContent[i].Equals(nul))
                    length--;
                break;
            }

            return encoder.GetString(byteContent, begin, length);
        }

        public static Frame CreateV3Frame(Stream stream)
        {
            // Frame ID   $xx xx xx (3 characters) 
            var frameIDBytes = read(stream, 3);

            if (frameIDBytes[0] == 0)
                return null;

            string frameId = ByteHelper.BytesToString(frameIDBytes);

            Frame F = new Frame(frameId);

            byte[] size = new byte[4];
            stream.Read(size, 1, 3);

            // Size                               $xx xx xx 00
            F.DataSize = ByteHelper.GetInt32FromBytes(size);

            if (frameId == "PIC")
            {
                F.FrameData = "Images - currently not supported";
                stream.Seek(F.DataSize, SeekOrigin.Current);
                
            }
            else 
                F.FrameData = readTextFrame(stream, F.DataSize);

            return F;
        }
    }
}
