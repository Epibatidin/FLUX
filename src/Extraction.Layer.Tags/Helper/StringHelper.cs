﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extraction.Layer.Tags.Helper
{
    public static class StringHelper
    {
        public static byte[] Read(this Stream stream, int length)
        {
            var result = new byte[length];

            stream.Read(result, 0, length);

            return result;
        }


        public static Encoding ReadEncoding(Stream stream, ref byte read)
        {
            // read 1 byte => textencoding
            switch (stream.Read(1)[0])
            {
                case 0:  // "ISO-8859-1" == ASCII 
                    read = 1;
                    var encoder = Encoding.GetEncoding("ISO-8859-1");
                    return encoder;
                case 1:  // UniCode
                    {
                        var endian = stream.Read(2);
                        read = 3;
                        if (endian[0] == 255 && endian[1] == 254)//$FF FE => littleEndian = 255, 254
                            return Encoding.Unicode;
                        else if (endian[0] == 254 && endian[1] == 255)//$FE FF => bigEndian = 254,255
                            return Encoding.BigEndianUnicode;
                    }
                    break;
            }
            return null;
        }


    }

}
