
using System.Text;
using System;
using System.Collections.Generic;
using System.Collections;
namespace Helper
{
    public static class ByteHelper
    {
        [System.Diagnostics.DebuggerStepThrough]
        public static bool GetBit(this byte _byte, int bitNumber)
        {
            return (_byte & (1 << bitNumber)) != 0;
        }
        
        [System.Diagnostics.DebuggerStepThrough]
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

        [System.Diagnostics.DebuggerStepThrough]
        public static byte[] GetByteArrayWith7SignificantBitsPerByteForInt(int length)
        {
            BitArray source = new BitArray(BitConverter.GetBytes(length));
            BitArray result = new BitArray(32);

            int offset = -1;
            for (int i = 0; i < source.Length - offset -1 ; i++)
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
            bits.CopyTo(ret, 0);
            Array.Reverse(ret);
            return ret;
        }

        /// <summary>
        /// encodes Bytes to int in Big Endian order
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerStepThrough]
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
        [System.Diagnostics.DebuggerStepThrough]
        public static byte[] GetBytesFromInt32(Int32 value)
        {
            // reverse array 
            var result = BitConverter.GetBytes(value);
            Array.Reverse(result);
            return result;
        }



        [System.Diagnostics.DebuggerStepThrough]
        public static string BytesToString(byte[] Values)
        {
            return BytesToString(Values, Encoding.GetEncoding("iso-8859-1"));
        }

        [System.Diagnostics.DebuggerStepThrough]
        public static string BytesToString(byte[] Values, Encoding encoder)
        {
            string nonZero = encoder.GetString(Values);
            // dann steuerzeichen
            return RemoveControlChars(nonZero).TrimEnd();
        }

        [System.Diagnostics.DebuggerStepThrough]
        private static string RemoveControlChars(string source)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < source.Length; i++)
            {
                char? ch = ToRelevant(source[i]);
                if(ch.HasValue)
                    builder.Append(ch);
            }
            return builder.ToString();
        }

        [System.Diagnostics.DebuggerStepThrough]
        private static char? ToRelevant(char ch)
        {
            if (ch == '\0')
                return ' ';
            else
            {
                if (char.IsControl(ch))
                    return null;
                else
                    return ch;
            }
        }


        [System.Diagnostics.DebuggerStepThrough]
        public static byte[] StringToByte(string Value)
        {
            return StringToByte(Value, Encoding.GetEncoding("iso-8859-1"));
        }
        
        [System.Diagnostics.DebuggerStepThrough]
        public static byte[] StringToByte(string Value, Encoding encoder)
        {
            return encoder.GetBytes(Value);
        }      
     
    }
}
