using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using MP3Renamer.Config;

namespace MP3Renamer.FileIO.WriteExecution
{
    public class MOQCopy : CopyHelperBase
    {
        public override void Copy(string From, string To)
        {
            //SwitchOrCreateDir(SuperRootFolder);
            DirectoryInfo dir = Directory.CreateDirectory(SuperRootFolder);
            To = From.Substring(Configuration.SourceFolder.Length);
                //Replace(Configuration.SourceFolder, SuperRootFolder);
            var dirs = To.Split('\\');
            To = SuperRootFolder;
            for (int i = 0; i < dirs.Length - 1; i++)
            {
                dir = base.ProtectedSwitchOrCreateDir(dir, dirs[i]);
                To = Path.Combine(To, dirs[i]);
            }
            To = Path.Combine(To, dirs[dirs.Length - 1]);

            FileInfo info = new FileInfo(To);
            if (!info.Exists)
            {
                using (Stream stream = info.Create())
                {
                    stream.WriteByte(0);
                    stream.Flush();
                }
            }
        }

       
        protected override DirectoryInfo ProtectedSwitchOrCreateDir(DirectoryInfo baseDir,
            string newFolderName)
        {
            return new DirectoryInfo(Path.Combine(baseDir.FullName, newFolderName));
        }

        public override string SuperRootFolder
        {
            get 
            {
                return Configuration.MoqFolder;
            }
        }
    }
}