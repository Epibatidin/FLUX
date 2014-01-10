using System;
using System.IO;
using System.Linq;
using MP3Renamer.Config;

namespace MP3Renamer.FileIO
{
    public enum FileType { NotSupported = 0, Music = 1, Picture = 2, Video = 3 }

    public static class ExtensionHelper
    {
        //-----------------------------------------------------------------------------------------------------------------------
        public static bool IsSupportedFile(FileInfo FInfo)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            if (FInfo != null)
            { 
                return checkExtensionType(RemoveDot(FInfo.Extension)) != FileType.NotSupported;
            }
            return false;
        }

        //-----------------------------------------------------------------------------------------------------------------------
        public static FileType getTypeOfExtension(string extension)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            if (!String.IsNullOrWhiteSpace(extension))
            {
                return checkExtensionType(RemoveDot(extension));               
            }
            return FileType.NotSupported;
        }

        private static string RemoveDot(string DottedExtension)
        {
            return DottedExtension.Remove(0, 1).ToLower();
        }

        private static FileType checkExtensionType(string extension)
        {
            if (Configuration.MusicExtensions.Contains(extension))
                return FileType.Music;

            return FileType.NotSupported;
        }

    }
}