//using System.Collections.Generic;
//using System.IO;

//namespace Extension.TagProcessing.TagWriter
//{
//    public class ID3V23TagWriter
//    {
//        public Stream _result;
//        public ID3V23TagWriter(Stream s)
//        {
//            _result = s;
//        }

//        private void writeID3StartTagV(byte version)
//        {
//            _result.Seek(0, SeekOrigin.Begin);
//            _result.Write(ByteHelper.StringToByte("ID3"));
//            //                Version 3.0   Flags  dummy size    
//            _result.Write(new byte[] { 3, 0, 0, 0, 0, 0, 0 }, 0, 7);
//        }

//        public void WriteFrame(List<Frame> frames)
//        {
//            writeID3StartTagV(2);
//            long lastPos = 0;
//            long totalFrameSize = 0;

//            // fang hinten an und schreib die tags extra die 

//            foreach (Frame f in frames)
//            {
//                lastPos = _result.Position;
//                FrameObjectToByteArray(f);
//                totalFrameSize += _result.Length - lastPos;
//            }
//            lastPos = _result.Position;
//            _result.Seek(6, SeekOrigin.Begin);
//            _result.Write(ByteHelper.GetByteArrayWith7SignificantBitsPerByteForInt((int)totalFrameSize));

//            // frame fertig geschrieben jetzt müssen die daten rein => 
//            // sourcestream an die stelle spulen und lesen bis endpos 
//            _result.Seek(lastPos,SeekOrigin.Begin);
//        }
        


//        private void WriteIDV31Tag(Stream Target)
//        {
//            Target.Seek(0, SeekOrigin.End);
//            //Target.Seek(0, SeekOrigin.End);
//            Target.Write(ByteHelper.StringToByte("TAG"));

//            string length125 = "ABCDEFGHI ABCDEFGHI ABCDEFGHI ABCDEFGHI ABCDEFGHI ABCDEFGHI ABCDEFGHI ABCDEFGHI ABCDEFGHI ABCDEFGHI ABCDEFGHI ABCDEFGHI 20091";

//            Target.Write(ByteHelper.StringToByte(length125));
//        }


//        private void FrameObjectToByteArray(Frame F)
//        {
//            var byteValues = ByteHelper.StringToByte(F.FrameData);
//            int DataSize = byteValues.Length + 1;

//            //  Frame ID       $xx xx xx xx (four characters) 
//            _result.Write(ByteHelper.StringToByte(F.FrameID));
//            //  Size           $xx xx xx xx

//            var blub = ByteHelper.GetBytesFromInt32(DataSize);
//            _result.Write(blub);
//            //  Flags          $xx xx
//            // keine flags
//            _result.Write(new byte[] { 0, 0 }, 0, 2);
//            // und das wichtigste die DATEN
//            // als erstes das (die) extra encoder byte(s)
//            _result.Write(new byte[] { 0 });
//            // dann die daten
//            _result.Write(byteValues);
//        }


//    }
//}
