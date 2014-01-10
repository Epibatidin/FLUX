using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Helper;

namespace FrameHandler.MainFrameDetection
{
    public static class DetectionMethods
    {
        public static bool IsID3V1(Stream stream)
        {
            stream.Seek(-128, SeekOrigin.End);
            var TagType = stream.Read(3);

            //                     T                   A                  G
            return (TagType[0] == 84 && TagType[1] == 65 && TagType[2] == 71);
        }

        public static bool IsID3(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var TagType = stream.Read(3);
            //                     I                   D                  3
            return (TagType[0] == 73 && TagType[1] == 68 && TagType[2] == 51);
        }

        public static bool IsID3V2(Stream stream)
        {
            return CheckForVersion(stream, 2);
        }

        public static bool IsID3V3(Stream stream)
        {
            return CheckForVersion(stream, 3);
        }

        
        public static bool IsID3V4(Stream stream)
        {
            return CheckForVersion(stream, 4);
        }

        private static bool CheckForVersion(Stream stream, byte Version)
        {
            stream.Seek(3, SeekOrigin.Begin);
            var version = stream.Read(2);
            return version[0] == Version;
        }

    }
}
