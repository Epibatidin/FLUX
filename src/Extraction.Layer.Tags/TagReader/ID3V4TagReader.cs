using System;
using System.IO;
using Extraction.Layer.Tags.DomainObjects;
using Extraction.Layer.Tags.Helper;
using System.Collections.Generic;

namespace Extraction.Layer.Tags.TagReader
{
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

                while (stream.Position < tagData.DataStart)
                {
                    var frame = CreateV3Frame(stream);
                    if (frame == null) break;
                    if (string.IsNullOrEmpty(frame.FrameData)) continue;
                    if (!FrameMapper.IsSupported(frame.FrameID)) continue;
                    
                    tagData.Frames.Add(frame);
                }
                return tagData;
            
        }

        private long EvaluateBeginOfData(Stream stream)
        {
            stream.Seek(5, SeekOrigin.Begin);

            var flags = read(stream, 1)[0];
            bool unsynchronisation = ByteHelper.GetBit(flags, 7);
            bool compression = ByteHelper.GetBit(flags, 6);

            var size = new byte[4];
            stream.Read(size, 0, 4);
            var length = ByteHelper.GetIntFrom7SignificantBitsPerByte(size) + 10;

            //if (flag)
            //{
            //    stream.Read(size, 0, 4);
            //    length += ByteHelper.GetIntFrom7SignificantBitsPerByte(size) + 6;
            //}
            return length;
        }

        private string readTextFrame(Stream stream, int DataSize)
        {
            byte read = 0;
            var encoding = StringHelper.ReadEncoding(stream, ref read);
            if (encoding == null)
                throw new Exception("Cannot read Encoder");

            // so jetzt die eigentliche Information lesen 
            return ByteHelper.BytesToString(stream.Read(DataSize - read), encoding);
        }
        
        private Frame CreateV3Frame(Stream stream)
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

            if (frameId[0] == 'T')
            {
                F.FrameData = readTextFrame(stream, F.DataSize);
            }
            else if (frameId == "APIC")
            {
                //<Header for 'Attached picture', ID: "APIC"> 
                //Text encoding   $xx
                //MIME type       <text string> $00
                //Picture type    $xx
                //Description     <text string according to encoding> $00 (00)
                //Picture data    <binary data>

                F.FrameData = "Images - currently not supported";
                stream.Seek(F.DataSize, SeekOrigin.Current);
            }
            else
                F.FrameData = ByteHelper.BytesToString(read(stream, F.DataSize));

            return F;
        }

        //        private string readTextFrame(Stream S, int DataSize)
        //        {
        //            byte read = 0;
        //            var encoding = StringHelper.ReadEncoding(S, ref read);
        //            if (encoding == null)
        //                throw new Exception("Cannot read Encoder");

        //            // so jetzt die eigentliche Information lesen 
        //            return ByteHelper.BytesToString(S.Read(DataSize - read), encoding);
        //        }


    }
}
