using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Collections;

namespace DFRenamer.FileAccess
{
    class FileFactory
    {
        string BasicFolderPath = "";
        List<FileAccess.Folder> FolderList;
        List<string> EqualStrings;

        public FileFactory(string FolderPath, string FactoryKind)
        {
            FolderList = new List<FileAccess.Folder>();
            EqualStrings = new List<string>();
            this.BasicFolderPath = FolderPath;
            processAllFiles();
        }

        private void getSubFolders(string folderPath)
        {
            string[] subdirs = Directory.GetDirectories(folderPath);

            foreach (string subdir in subdirs) getSubFolders(subdir);

            createFolder(folderPath);
        }
      
        private void createFolder(string folder)
        {
            // schneid den folder auseinander und such nach jahr und album
            string[] dummy = folder.Split('\\');
            int depth = dummy.Length - 1;
            // an letzte position steht entweder das gesuchte oder cd
            string abl = dummy[depth];
            if (abl.Contains("CD") || abl.Contains("cd") || abl.Contains("Cd"))
            {
                addFolder
                    (folder.Substring(0,folder.LastIndexOf('\\')),  // alles ausser cd
                    true);  // cd
                return;
            }
            addFolder(folder, false);
        }

        private void addFolder(string path, bool hasMultipleCD)
        {
            FileAccess.Folder f = FileAccess.Folder.createFolder(path, hasMultipleCD);
            if (f != null) FolderList.Add(f);
        }


        private void getAllFiles()
        {
            getSubFolders(BasicFolderPath);
        }

        private void processAllFiles()
        {           
            getAllFiles();           
        }

        public void gatherInformation()
        {
            // ich such als erstes nach den künstlern 
            // diese müssen in allen foldern gleich sein 
            // ich brauch ein feld in dem ich alle gleichnisse speichere

            // einmal im kreis vergleichen !

            int folderCount = FolderList.Count;

            foreach (var s in FolderList)
            {
                System.Console.WriteLine("MH " + s.Album);
            }



            for(int i = 0; i< folderCount; i++)
            {
                System.Console.WriteLine("1");
                string[] Aparts = FolderList[i % folderCount].AlbumParts;
                string[] Bparts = FolderList[(i + 1) % folderCount].AlbumParts;
                
                if (Aparts.Length >= Bparts.Length)
                    findEqualStrings(Aparts,Bparts);
                else
                    findEqualStrings(Bparts,Aparts);
                
            }

            int k = 0;
            /*foreach( string s in EqualStrings)
            {
                System.Console.WriteLine(s + " " + (k++) );
            }*/

        }

        private void findEqualStrings(string[] aparts, string[] bparts)
        {
            foreach (string a in aparts)
            {
                foreach (string b in bparts)
                {
                    if (a.Equals(b)) EqualStrings.Add(a);
                }
            }           
        }
    }
}
