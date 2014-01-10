using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace MP3Renamer.Helper
{
    public class ByteHelper
    {

        public static byte[] calculateFrameSizeWithSevenBits(int FrameSize)
        {
            int dummyLength = 3;
            byte[] output = new byte[4];
            // hier interessant geht aber keine ahnung warum
            // konvertiert 8bit pro byte dinger in 7 bit je byte dinger

            for (int j = 0; j < sizeof(int); j++)
            {
                byte b = (byte)(FrameSize & 0x7f);
                output[dummyLength--] = b;
                FrameSize >>= 7;
            }
            return output;
        }


        public static int calculateFrameSizeWithSevenBits(byte[] FrameSize)
        {
            int sum = 0;
            foreach (var b in FrameSize)
            {
                sum <<= 7;
                sum = sum + (b & 0x7f);
            }
            return sum;
        }

        public static string ByteArrayToString(byte[] input)
        {
            if (input.Length == 0) return "";
            StringBuilder buf = new StringBuilder(input.Length, input.Length);
            for (int i = 0; i < input.Length; i++)
            {
                if(input[i] != 0)
                    buf.Append((char)input[i]);
            }
            string s = buf.ToString();
            return s;
        }


        public static Encoding ReadEncoder(byte[] EncoderInfo)
        {
            if (EncoderInfo.Length >= 1)
            {
                if (EncoderInfo[0] == 0) // "ISO-8859-1" == ASCII 
                {
                    return Encoding.GetEncoding("ISO-8859-1");
                }
                else if (EncoderInfo[0] == 1) // UniCode
                {
                    if (EncoderInfo.Length > 2)
                    {
                        if (EncoderInfo[1] == 255 && EncoderInfo[2] == 254)//$FF FE => littleEndian = 255, 254
                        {
                            return Encoding.Unicode;
                        }
                        else if (EncoderInfo[1] == 254 && EncoderInfo[2] == 255)//$FE FF => bigEndian = 254,255
                        {
                            return Encoding.BigEndianUnicode;
                        }
                    }
                }
            }
            return null;
        }



    }
}