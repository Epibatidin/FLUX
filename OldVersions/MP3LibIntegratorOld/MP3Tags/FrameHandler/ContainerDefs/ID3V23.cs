using System.Collections.Generic;
using System.IO;
using Helper;
using FrameHandler.Frames;
using System;
using System.Linq;

namespace FrameHandler.ContainerDefs
{
    internal class ID3V23 : Container
    {
        private bool IDV1HeaderFound;

        protected override void InitStream()
        {
            // get data start 
            byte flags = new byte();
            var framesize = GetDataLength(out flags);
            DataStart = framesize + GetExtendedHeaderLength(flags) + 10;
            DataEnd = GetDataEnd();
        } 


        public override void ReadFrameCollection()
        {
            // tags und version hab ich gelesen 
            // jetzt muss ich den nächsten byte skippen weil der uninteressante flags enthält
            SourceStream.Seek(10, SeekOrigin.Begin);
           
            while (true)
            {
                if (SourceStream.Position == DataStart)
                    break;

                Frame F = CreateV3Frame(SourceStream);
                if (F != null)
                    Frames.Add(F);
                else
                    break;
            }
            if (IDV1HeaderFound)
                CreateV1Frame(SourceStream);

            SourceStream.Seek(DataStart, SeekOrigin.Begin);
        }

        protected override void InternalWrite(Stream Target)
        {
            Target.Seek(0, SeekOrigin.Begin);
            Target.Write(ByteHelper.StringToByte("ID3"));
            //                Version 3.0   Flags  dummy size    
            Target.Write(new byte[] { 3,0,  0,     0, 0, 0, 0  }, 0, 7); 
            
            long totalFrameSize = 0;
            Stream frameStream = null; 
            foreach(Frame f in Frames)
            {
                frameStream = FrameObjectToByteArray(f);
                totalFrameSize += frameStream.Length;
                frameStream.CopyTo(Target);
                frameStream.Flush();
            }
            Target.Seek(6, SeekOrigin.Begin);
            Target.Write(ByteHelper.GetByteArrayWith7SignificantBitsPerByteForInt((int)totalFrameSize));

            // frame fertig geschrieben jetzt müssen die daten rein => 
            // sourcestream an die stelle spulen und lesen bis endpos 
            Target.Seek(DataStart, SeekOrigin.Begin);

            // Target.Copy(SourceStream, DataStart, DataEnd);
            
          
            Target.Flush();
            Target.Close();
        }


        private void WriteIDV31Tag(Stream Target)
        {
            Target.Seek(0, SeekOrigin.End);
            //Target.Seek(0, SeekOrigin.End);
            Target.Write(ByteHelper.StringToByte("TAG"));

            string length125 = "ABCDEFGHI ABCDEFGHI ABCDEFGHI ABCDEFGHI ABCDEFGHI ABCDEFGHI ABCDEFGHI ABCDEFGHI ABCDEFGHI ABCDEFGHI ABCDEFGHI ABCDEFGHI 20091";

            Target.Write(ByteHelper.StringToByte(length125));
        }


        private Stream FrameObjectToByteArray(Frame F)
        {
            var byteValues = ByteHelper.StringToByte(F.FrameData);
            int DataSize = byteValues.Length + 1;

            MemoryStream temp = new MemoryStream();
            //  Frame ID       $xx xx xx xx (four characters) 
            temp.Write(ByteHelper.StringToByte(F.FrameID));
            //  Size           $xx xx xx xx

            var blub = ByteHelper.GetBytesFromInt32(DataSize);
            temp.Write(blub);
            //  Flags          $xx xx
            // keine flags
            temp.Write(new byte[] {0, 0} , 0 , 2);
            // und das wichtigste die DATEN
            // als erstes das (die) extra encoder byte(s)
            temp.Write(new byte[] { 0 });
            // dann die daten
            temp.Write(byteValues);
            temp.Seek(0, SeekOrigin.Begin);


            return temp;
        }


        #region CreateFrames
        private Frame CreateV3Frame(Stream stream)
        {
            // Frame ID   $xx xx xx xx (four characters) 
            var frameIDBytes = stream.Read(4);

            if (frameIDBytes[0] == 0)
                return null;

            string FrameID = ByteHelper.BytesToString(frameIDBytes);           

            Frame F = new Frame(FrameID);
            // Size                               $xx xx xx xx
            int DataSize = ByteHelper.GetInt32FromBytes(stream.Read(4));
                        
            // Flags          $xx xx
            F.SetFlags(stream.Read(2));
            
            if (FrameID[0] == 'T')
                F.FrameData = readTextFrame(stream, DataSize);
            else if (FrameID == "APIC")
            {
                //<Header for 'Attached picture', ID: "APIC"> 
                //Text encoding   $xx
                //MIME type       <text string> $00
                //Picture type    $xx
                //Description     <text string according to encoding> $00 (00)
                //Picture data    <binary data>

                F.FrameData = "Images - currently not supported";
                stream.Seek(DataSize, SeekOrigin.Current);
            }
            else
                F.FrameData = ByteHelper.BytesToString(stream.Read(DataSize));
            return F;
        }


        private void CreateV1Frame(Stream stream)
        {
            // ID1 header am ende ... 
            stream.Seek(-125, SeekOrigin.End);
            // 30 zeichen title     => TIT2    
            // 30 zeichen artist    => TCOM
            // 30 zeichen album     => TALB    
            // 4 zeichen jahr       => TYER    
            // 30 zeichen commentar - kann auch den track enthalten (TRCK) - der letzte byte
            // 1 byte Genre 
            // == 125 

            foreach (var item in new[] { "TIT2", "TCOM", "TALB" })
            {
                AddNonEmptyFrame(item, stream.Read(30));
            }
            AddNonEmptyFrame("TYER", stream.Read(4));

            // comment - egal
            stream.Seek(28, SeekOrigin.Current);
            var track = stream.Read(2);
            if (track[0] == 0 && track[1] != 0)
            {
                // dann ist track[1] eine zahl;
                if (track[1] < 48)
                {
                    Frame f = new Frame("TRCK");
                    f.FrameData = ((int)track[1]).ToString();
                    Frames.Add(f);
                }
            }
        }
        #endregion

        #region Helper
        private int GetDataLength(out byte flags)
        {
            SourceStream.Seek(5, SeekOrigin.Begin);
            flags = SourceStream.Read(1)[0];
            var Size = SourceStream.Read(4);

            int length = ByteHelper.GetIntFrom7SignificantBitsPerByte(Size);
            return length;
        }

        private int GetExtendedHeaderLength(byte Flags)
        {
            int length = 0;
            if (Flags.GetBit(7))
            {
                // vorspulen;
                var size = SourceStream.Read(4);

                //                length                                    + rest of header
                length = ByteHelper.GetIntFrom7SignificantBitsPerByte(size) + 6;
            }
            return length;
        }

        private long GetDataEnd()
        {            
            SourceStream.Seek(-128, SeekOrigin.End);
            var TagType = SourceStream.Read(3);

            //                T                   A                    G
            if (TagType[0] == 84 && TagType[1] == 65 && TagType[2] == 71)
            {
                IDV1HeaderFound = true;
                return SourceStream.Length - 128;
            }
            else
                return SourceStream.Length;
        }


        private void AddNonEmptyFrame(string FrameID, byte[] Value)
        {
            var framedata = ByteHelper.BytesToString(Value);
            if (String.IsNullOrEmpty(framedata))
                return;

            Frame f = new Frame(FrameID);
            f.FrameData = framedata;
            Frames.Add(f);
        }


        private string readTextFrame(Stream S, int DataSize)
        {
            byte read = 0;
            var encoding = StringHelper.ReadEncoding(S, ref read);
            if (encoding == null)
                throw new Exception("Cannot read Encoder");

            // so jetzt die eigentliche Information lesen 
            return ByteHelper.BytesToString(S.Read(DataSize - read), encoding);
        }        
        #endregion

    }
}