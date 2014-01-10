using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MP3Renamer.FileIO.WriteExecution
{
    public abstract class CopyHelperBase 
    {
        public abstract void Copy(string From, string To);

        public abstract string SuperRootFolder { get; }

        //public virtual DirectoryInfo SwitchOrCreateDir(string Path)
        //{
        //    if (!Directory.Exists(Path))
        //        return Directory.CreateDirectory(Path);
        //    else
        //        return new DirectoryInfo(Path);
        //}


        public DirectoryInfo SwitchOrCreateDir(string newFolderName)
        {
            DirectoryInfo info = new DirectoryInfo(newFolderName);
            return SwitchOrCreateDir(info, newFolderName);
        }

        public DirectoryInfo SwitchOrCreateDir(DirectoryInfo baseDir, string newFolderName)
        {
            return ProtectedSwitchOrCreateDir(baseDir, newFolderName);
        }

        protected virtual DirectoryInfo ProtectedSwitchOrCreateDir(DirectoryInfo baseDir,
            string newFolderName)
        {
            DirectoryInfo info = new DirectoryInfo(Path.Combine(baseDir.FullName, newFolderName));
            if (!info.Exists)
            {
                info.Create();
            }
            return info;
        }

    }
}
