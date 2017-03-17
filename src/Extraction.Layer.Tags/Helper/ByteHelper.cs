using System;
using System.Collections;
using System.Text;

namespace Extraction.Layer.Tags.Helper
{
    public static class ByteHelper
    {       
        public static bool GetBit(byte value, int bitNumber)
        {
            return (value & (1 << bitNumber)) != 0;
        }

        
        public static int GetIntFrom7SignificantBitsPerByte(byte[] bytes)
        {
            int tagSize = 0;

            for (int i = 0; i < bytes.Length; i++)
            {
                tagSize = tagSize << 7;
                tagSize |= (bytes[i] & 0x7f);
            }
            return tagSize;
        }

        public static string BytesToString(byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes, 0, bytes.Length);
        }

        public static string BytesToString(byte[] bytes)
        {
            return BytesToString(bytes, Encoding.ASCII);
        }

        
        public static byte[] GetByteArrayWith7SignificantBitsPerByteForInt(int length)
        {
            BitArray source = new BitArray(BitConverter.GetBytes(length));
            BitArray result = new BitArray(32);

            int offset = -1;
            for (int i = 0; i < source.Length - offset - 1; i++)
            {
                if (i % 7 == 0)
                    offset++;

                result[i + offset] = source[i];
            }
            return BitArrayToByteArray(result);
        }

        private static byte[] BitArrayToByteArray(BitArray bits)
        {
            byte[] ret = new byte[bits.Length / 8];
            //bits.CopyTo(ret, 0);
            Array.Reverse(ret);
            return ret;
        }

        /// <summary>
        /// encodes Bytes to int in Big Endian order
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>        
        public static int GetInt32FromBytes(byte[] bytes)
        {
            Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        /// encodes Bytes to int in Big Endian order
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>       
        public static byte[] GetBytesFromInt32(Int32 value)
        {
            // reverse array 
            var result = BitConverter.GetBytes(value);
            Array.Reverse(result);
            return result;
        }
    }
}
