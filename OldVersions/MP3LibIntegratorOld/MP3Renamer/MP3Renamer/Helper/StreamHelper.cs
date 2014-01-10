using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace MP3Renamer.Helper
{
    public static class StreamHelper
    {
        public static byte[] Read(Stream stream, int count)
        {
            byte[] result = new byte[count];

            stream.Read(result, 0, count);

            return result;
        }

    }
}