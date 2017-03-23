using System;
using System.IO;
using Extraction.Layer.Tags.DomainObjects;
using Extraction.Layer.Tags.Helper;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Extraction.Layer.Tags.TagReader
{
    // http://id3.org/id3v2.4.0-structure
    public class ID3V4TagReader : ID3TagReader
    {
        public ID3V4TagReader() : base(4)
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
                i++;
                var frame = CreateFrame(stream);
                if (frame == null) break;

                if (ShouldAddFrame(frame))
                    tagData.Frames.Add(frame);
            }
            return tagData;

        }

        private long EvaluateBeginOfData(Stream stream)
        {
            stream.Seek(5, SeekOrigin.Begin);

            var flags = new BitArray(read(stream, 1));

            if (flags[7]) // unsyncronisation
                throw new NotSupportedException("Dont know what this flag means");

            if (flags[6]) // extended Header
                throw new NotImplementedException("Will follow someday");

            if (flags[5]) // experimental
                throw new NotImplementedException("Will follow someday");

            if (flags[4]) // footer Exists
                throw new NotImplementedException("Will follow someday");

            var size = new byte[4];
            stream.Read(size, 0, 4);
            var length = ByteHelper.GetIntFrom7SignificantBitsPerByte(size) + 10;

            return length;
        }



        private static string readTextFrame(Stream stream, int dataSize)
        {
            var byteContent = read(stream, dataSize);

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
                    begin = 3;
                    length -= 3;
                    if (byteContent[1] == 255 && byteContent[2] == 254)//$FF FE => littleEndian = 255, 254
                        encoder = Encoding.Unicode;
                    else if (byteContent[1] == 254 && byteContent[2] == 255)//$FE FF => bigEndian = 254,255
                        encoder = Encoding.BigEndianUnicode;
                    break;
                case 2:
                    begin = 1;
                    length--;
                    encoder = Encoding.BigEndianUnicode;
                    break;
                case 3:
                    begin = 1;
                    length -= 1;
                    encoder = Encoding.UTF8;
                    break;
            }
            return encoder.GetString(byteContent, begin, length);
        }

        public static Frame CreateFrame(Stream stream)
        {
            // Frame ID   $xx xx xx xx (four characters) 
            var frameIDBytes = read(stream, 4);

            if (frameIDBytes[0] == 0)
                return null;

            string frameId = ByteHelper.BytesToString(frameIDBytes);

            Frame F = new Frame(frameId);
            // Size                               $xx xx xx xx
            F.DataSize = ByteHelper.GetInt32FromBytes(read(stream, 4));

            // Flags          $xx xx
            F.Flags = read(stream, 2);

            if (F.Flags[1] != 0)
                throw new NotImplementedException("So many flag stuff - this is relevant stuff like encryption");


            if (frameId == "APIC")
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
