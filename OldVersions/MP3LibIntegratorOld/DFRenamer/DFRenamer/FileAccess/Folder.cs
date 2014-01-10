using System.IO;
using System.Collections.Generic;
using System.Collections;
using System;

namespace DFRenamer.FileAccess
{
    class Folder
    {
        // erzeuge einen neuen folder 
        // aber nur wenn es ihn noch nicht gibt

        private static List<string> knownFolder = new List<string>();
        private static char[] splitAt = new char[] {' ', '_', '-'};
        public static Folder createFolder(string path, bool hasMultipleCD)
        {
            if (path == "") return null;
            foreach (string folder in knownFolder)
            {
                if (folder == path)
                {
                    return null;
                }
            }
            knownFolder.Add(path);

            return new Folder(path, hasMultipleCD);
        }


        private string AlbumFolder = "";
        private bool hasMultipleCDs;
        private ArrayList files;
        private string[] parts;

        public Folder(string album,bool hasMultipleCD)
        {
            this.hasMultipleCDs = hasMultipleCD;
            this.AlbumFolder = album;
            files = new ArrayList();
            createInfoStubs();
            trimAlbumFolder();
        }

        public string Album
        {
            get
            {
                return AlbumFolder;
            }
        }

        public string[] AlbumParts
        {
            get
            {
                if (parts == null)
                {
                    parts = AlbumFolder.Split(splitAt, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < parts.Length; i++)
                    {
                        parts[i] = parts[i].Trim();
                    }
                }

                return parts;
            }
        }


        private void trimAlbumFolder()
        {
            //wenn cd dann eins tiefer
            string[] dummy = AlbumFolder.Split('\\');
            AlbumFolder = dummy[dummy.Length - 1];           
            System.Console.WriteLine(AlbumFolder);
        }

        private void createInfoStubs()
        {
            if (hasMultipleCDs)
            {
                string[] subdirs = Directory.GetDirectories(AlbumFolder);
                foreach (string subdir in subdirs)
                {
                    OneFolderInfo(Directory.GetFiles(subdir, "*.mp3"));
                }
            }
            else
            {
                OneFolderInfo(Directory.GetFiles(AlbumFolder, "*.mp3"));
            }
        }

        private void OneFolderInfo(string[] FullPath)
        {
            if (FullPath == null) return;
            foreach (string path in FullPath)
                if(path != string.Empty) 
                    files.Add(new InfoStorage(path)); 
        }
        
    }
}
