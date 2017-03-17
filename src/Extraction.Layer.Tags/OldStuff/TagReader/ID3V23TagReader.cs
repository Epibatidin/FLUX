//using System;
//using System.IO;

//namespace Extension.TagProcessing.TagReader
//{
//    public class ID3V23TagReader : BaseTagReader
//    {
//        private bool IDV1HeaderFound;


//        protected override MP3Data internalReadFrame()
//        {
//            byte flags = new byte();
//            var framesize = GetDataLength(out flags);
//            long DataStart = framesize + GetExtendedHeaderLength(flags) + 10;
//            long DataEnd = GetDataEnd();

//            // tags und version hab ich gelesen 
//            // jetzt muss ich den nächsten byte skippen weil der uninteressante flags enthält
//            _source.Seek(10, SeekOrigin.Begin);

//            while (true)
//            {
//                if (_source.Position == DataStart)
//                    break;

//                Frame F = CreateV3Frame(_source);
//                if (F != null)
//                    AddFrame(F);
//                else
//                    break;
//            }
//            if (IDV1HeaderFound)
//                CreateV1Frames(_source);

//            _source.Seek(DataStart, SeekOrigin.Begin);
//            return new MP3Data()
//            {
//                DataStart = DataStart,
//                DataEnd = DataEnd
//            };
//        }

//        private Frame CreateV3Frame(Stream stream)
//        {
//            // Frame ID   $xx xx xx xx (four characters) 
//            var frameIDBytes = stream.Read(4);

//            if (frameIDBytes[0] == 0)
//                return null;

//            string FrameID = ByteHelper.BytesToString(frameIDBytes);

//            Frame F = new Frame(FrameID);
//            // Size                               $xx xx xx xx
//            int DataSize = ByteHelper.GetInt32FromBytes(stream.Read(4));

//            // Flags          $xx xx
//            F.SetFlags(stream.Read(2));

//            if (FrameID[0] == 'T')
//                F.FrameData = readTextFrame(stream, DataSize);
//            else if (FrameID == "APIC")
//            {
//                //<Header for 'Attached picture', ID: "APIC"> 
//                //Text encoding   $xx
//                //MIME type       <text string> $00
//                //Picture type    $xx
//                //Description     <text string according to encoding> $00 (00)
//                //Picture data    <binary data>

//                F.FrameData = "Images - currently not supported";
//                stream.Seek(DataSize, SeekOrigin.Current);
//            }
//            else
//                F.FrameData = ByteHelper.BytesToString(stream.Read(DataSize));
//            return F;
//        }

//        private void CreateV1Frames(Stream stream)
//        {
//            // ID1 header am ende ... 
//            stream.Seek(-125, SeekOrigin.End);
//            // 30 zeichen title     => TIT2    
//            // 30 zeichen artist    => TCOM
//            // 30 zeichen album     => TALB    
//            // 4 zeichen jahr       => TYER    
//            // 30 zeichen commentar - kann auch den track enthalten (TRCK) - der letzte byte
//            // 1 byte Genre 
//            // == 125 

//            foreach (var item in new[] { "TIT2", "TCOM", "TALB" })
//            {
//                AddFrame(item, ByteHelper.BytesToString(stream.Read(30)));
//            }
//            AddFrame("TYER", ByteHelper.BytesToString(stream.Read(4)));

//            // comment - egal
//            stream.Seek(28, SeekOrigin.Current);
//            var track = stream.Read(2);
//            if (track[0] == 0 && track[1] != 0)
//            {
//                // dann ist track[1] eine zahl;
//                if (track[1] < 48)
//                {                
//                    AddFrame("TRCK",((int)track[1]).ToString());
//                }
//            }
//        }
        
//        #region Helper
//        private int GetDataLength(out byte flags)
//        {
//            _source.Seek(5, SeekOrigin.Begin);
//            flags = _source.Read(1)[0];
//            var Size = _source.Read(4);

//            int length = ByteHelper.GetIntFrom7SignificantBitsPerByte(Size);
//            return length;
//        }

//        private int GetExtendedHeaderLength(byte Flags)
//        {
//            int length = 0;
//            if (Flags.GetBit(7))
//            {
//                // vorspulen;
//                var size = _source.Read(4);

//                //                length                                    + rest of header
//                length = ByteHelper.GetIntFrom7SignificantBitsPerByte(size) + 6;
//            }
//            return length;
//        }

//        private long GetDataEnd()
//        {
//            _source.Seek(-128, SeekOrigin.End);
//            var TagType = _source.Read(3);

//            //                T                   A                    G
//            if (TagType[0] == 84 && TagType[1] == 65 && TagType[2] == 71)
//            {
//                IDV1HeaderFound = true;
//                return _source.Length - 128;
//            }
//            else
//                return _source.Length;
//        }

//        private string readTextFrame(Stream S, int DataSize)
//        {
//            byte read = 0;
//            var encoding = StringHelper.ReadEncoding(S, ref read);
//            if (encoding == null)
//                throw new Exception("Cannot read Encoder");

//            // so jetzt die eigentliche Information lesen 
//            return ByteHelper.BytesToString(S.Read(DataSize - read), encoding);
//        }
//        #endregion


//    }
//}
