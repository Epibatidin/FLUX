using System;
using System.IO;
using MP3Renamer.DataContainer.Root;
using MP3Renamer.FileIO.FormatProvider;
using MP3Renamer.FileIO.WriteExecution;
using System.Collections.Generic;

namespace MP3Renamer.FileIO
{
    public enum CopyType : byte
    {
        ZeroCopy = 0,
        TrueCopy = 1 , 
        MOQCopy = 2
    }

    public class FileStructureFactory
    {
        public int Process { get; private set; }

        #region CTor & Init
        private CopyHelperBase CopyHelper;
        private FormatProviderBase FormatProvider;

        private CopyHelperBase ParseCopyHelper(CopyType copyType)
        {
            switch (copyType)
            {
                case CopyType.ZeroCopy:
                    return new ZeroCopy();
                case CopyType.MOQCopy:
                    return new MOQCopy();
                case CopyType.TrueCopy:
                    return new TrueCopy();
                default:
                    throw new Exception("No CopyHelper Provided");
            }
        }

        private FormatProviderBase ParseFormatProvider(FileType _type)
        {
            switch (_type)
            {
                case FileType.Music:
                    return new MusicFormatProvider();
                case FileType.Video:
                    return new VideoFormatProvider();
                default:
                    throw new Exception("No FileType Provided");
            }
        }
        
        public FileStructureFactory(CopyType copyType)
        {
            CopyHelper = ParseCopyHelper(copyType);
            FormatProvider = ParseFormatProvider(FileType.Music);
        }
        #endregion

        private DirectoryInfo CreateFolder(DirectoryInfo baseDir, Dictionary<string, object> data)
        {
            DirectoryInfo dir = baseDir;

            var format = FormatProvider.ApplyDirFormat(data);

            for (int i = 0; i < format.Length; i++)
            {
                // für jedes format ding muss ich einen ordner erstellen 
                dir = CopyHelper.SwitchOrCreateDir(dir, format[i]);
               
            }
            return dir;
        }

        private FileHelper AssambleFromTo(DirectoryInfo baseDir, Dictionary<string, object> data)
        {
            var format = FormatProvider.ApplyFileFormat(data);

            if (format.Length == 2)
            {
                return new FileHelper(format[0], format[1], baseDir);
            }
            return null;
        }

        
        public void WriteToFileSystem()
        {
            TreeIterator iterator = new TreeIterator();
            DirectoryInfo dir = null;
            var superroot = CopyHelper.SwitchOrCreateDir(CopyHelper.SuperRootFolder);
            foreach (var item in iterator.IterateInWriteFileOrder())
            {
                if (item.Stop)
                    dir = CreateFolder(superroot, item.Data);

                var s = AssambleFromTo(dir, item.Data);
                if (s != null)
                {
                    CopyHelper.Copy(s.From, s.To);
                }
                Process++;
                // ich brauch jmd der mir das zeug übersetzt 
                // KEINE UNTERSCHEIDUNG WOHER DIE INFORMATON KOMMT !!!!!
                // abstraction war das ziel 
                // ich muss die daten jetzt nur noch an die richtige stelle schreiben 
                // und das macht natürlich mein FORMATHANDLER


            }
        }
    }
}