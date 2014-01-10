using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using MP3Renamer.Helper;

namespace MP3Renamer.FileIO.Read.MP3
{
    public class MP3Pattern
    {
        /*
                TRCK    [#TRCK Track number/Position in set]
                TIT2    [#TIT2 Title/songname/content description]  
                TALB    [#TALB Album/Movie/Show title]
                TCOM    [#TCOM Composer]
                TORY    [#TORY Original release year]
        */
        
        Dictionary<byte[], string> frames = null;

        private Encoder encoder; 



        public MP3Pattern()
        {
            frames = new Dictionary<byte[], string>();          
           
        }

        

        public Func<byte[], object> GetTranslator(int key)
        {
            switch (key)
            {
                case 0 :
                    return ID3Tag;
                case 1 :
                    return ID3Version;
                case 2 :
                    return ID3Size;                               
            }
            return null;
        }

        List<int> FrameStarts = new List<int>()
        {
            3 ,     // ID3v2/file identifier  "ID3" 
            3 ,     // ID3v2 version           $03 00
            4 ,     // ID3v2 size             4 * %0xxxxx
            0 ,     // stop   - pos 3 
            4 ,     // FrameHeader
            4 ,     // FrameSize 
            2       // FrameFlags
        };

        private object GetFrameSize(byte[] Size)
        {
            return ByteHelper.calculateFrameSizeWithSevenBits(Size);
        }

        public object DefFunc(byte[] blub)
        {
            return null;
        }

        private object ID3Tag(byte[] tag)
        {
            var s = ByteHelper.ByteArrayToString(tag);
            return s;
        }

        private object ID3Size(byte[] bytes)
        {
            return ByteHelper.calculateFrameSizeWithSevenBits(bytes);
        }

        //private object ID3Size(byte[] bytes)
        //{
        //    byte[] output = new byte[4];

        //    output[0] = bytes[3];
        //    output[1] = bytes[2];
        //    output[2] = bytes[1];
        //    output[3] = bytes[0];

        //    var result = BitConverter.ToInt32(output, 0);
        //    return 2350;
        //}



        private object ID3Version(byte[] version)
        {
            var s = ByteHelper.ByteArrayToString(version);
            return s;
        }

        
        
        public string ReadValueByKey(byte[] key, byte[] value)
        {
            if (frames.ContainsKey(key))
            {
                frames[key] = extractInfo(value); 
            }
            return "";

            //foreach (string ID3 in File.neededInformation.Keys)
            //{
            //    frames.Add(ID3, new MP3InfoFrame(ID3));
            //}            


            //return input;
        }





        private string extractInfo(byte[] value)
        {
            return "";
        }

        private void Pattern()
        {
            //switch (headerTyp)
            //{
            //    case "TRCK":
            //        short trck = checkInteger();
            //        if (trck > -1)
            //        {
            //            // track muss zweistellig sein
            //            if (trck < 100)
            //            {
            //                return true;
            //            }
            //        }
            //        break;
            //    case "TORY": // track und year sollen zahlen sein
            //        short tory = checkInteger();
            //        if (tory > -1)
            //        {
            //            // jahr muss 4 stellig sein
            //            if (tory > 1900)
            //            {
            //                return true;
            //            }
            //        }
            //        break;
            //}
        }


        
    }
}