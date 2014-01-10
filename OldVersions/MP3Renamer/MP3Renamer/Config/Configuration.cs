using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace MP3Renamer.Config
{
    public static class Configuration
    {   
        private static string rootdir;
        private static string RootDir
        {
            get
            {
                if (rootdir == null)
                {
                    rootdir = ConfigurationManager.AppSettings["RootDir"];
                }
                return rootdir;
            }
        }

        private static string AssambleConfigPath(string ConfigKey)
        {
            return Path.Combine(RootDir, ConfigurationManager.AppSettings[ConfigKey]);
        }


        private static string targetfolder;
        public static string TargetFolder
        {
            get
            {
                if(targetfolder == null)
                {
                    targetfolder = AssambleConfigPath("BaseTargetFolder");
                }
                return targetfolder;
            }
        }


        private static string sourcefolder;
        public static string SourceFolder
        {
            get
            {
                if(sourcefolder == null)
                {
                    sourcefolder = AssambleConfigPath("BaseSourceFolder");
                }
                return sourcefolder;
            }
        }

        private static string moqfolder;
        public static string MoqFolder
        {
            get
            {
                if (moqfolder == null)
                {
                    moqfolder = AssambleConfigPath("MOQTargetFolder");
                }
                return moqfolder;
            }
        }

        private static List<string> musicextensions;
        public static IEnumerable<string> MusicExtensions
        {
            get
            {
                if (musicextensions == null)
                {
                    musicextensions = ConfigurationManager.AppSettings["MusicExtensions"].Split(',').ToList();
                }
                return musicextensions;
            }
        }
    }
}