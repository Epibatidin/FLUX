using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;


namespace DFRenamer.FileAccess
{
    public class File
    {        
        //private MP3Tagger tagger;
        private Pattern pat;
        private FileInfo fi;

        public string oldFileName { get; set; }
        public string newFileName { get; set; }

        public string oldFolderPath { get; set; }
        public string newFolderPath { get; set; }

        private string FullPath;

        public string extension { get; set; }
        private string album;
        private string year;

        public static Dictionary<string,char> neededInformation = null;


        private static void setNeededInformation()
        {
            neededInformation = new Dictionary<string, char>();
            neededInformation.Add("TRCK", '#');   // Track Count
            neededInformation.Add("TIT2", 't');   // Titel
            neededInformation.Add("TALB", 'a');   // Album
            neededInformation.Add("TPE2", 'i');   // Interpret
            neededInformation.Add("TYER", 'y');   // Entstehungsjahr 
            neededInformation.Add("TCON", 'r');   // Genre r wie richtung weil g = garbage
            neededInformation.Add("TLEN", 'l');   // Länge
        }

        public File(string FileName)
        {
            //System.Console.WriteLine(FileName);
            this.FullPath = FileName;
            fi = new FileInfo(FileName);
            //if (neededInformation == null) setNeededInformation();
        }
        
        private void extractFileName()
        {
            oldFolderPath = Path.GetDirectoryName(FullPath) + "\\";
            extension = Path.GetExtension(FullPath);
            oldFileName = Path.GetFileNameWithoutExtension(FullPath);         
        }


       

        private void generateNewName()
        {
            System.Console.WriteLine();

            System.Console.WriteLine("============================================================");
            System.Console.WriteLine(oldFolderPath);
            System.Console.WriteLine(oldFileName);

            extractInformationFromFolder();

            pat = new Pattern(year, album);
            newFileName = pat.applyPatternOn(oldFileName);
            //newFolderPath =

            System.Console.WriteLine(newFolderPath);
            System.Console.WriteLine(newFileName);

        }


     


        private void extractInformationFromFolder()
        {
            // schneid den folder auseinander und such nach jahr und album
            string[] dummy = oldFolderPath.Split('\\');
            int depth = dummy.Length - 2;
            // an letzte position steht entweder das gesuchte oder cd
            string abl = dummy[depth];
            if (abl.Contains("CD") || abl.Contains("cd") || abl.Contains("Cd"))
            {
                depth--;
            }
            abl = dummy[depth];
            dummy = abl.Split('-');
            year = dummy[0].Trim();
            album = dummy[1].Trim();            
        }

        public void makeLIBConform(bool doIT)
        {
            extractFileName();
            generateNewName();
            reCreateLIBConformMP3(doIT);
            //if(doIT) fi.MoveTo(folder + NewName);
        }

        /*public void reCreateLIBConformMP3(bool doIT)
        {
            tagger = new MP3Tagger();
            tagger.readMP3(folder, oldName,extension);
            tagger.HandlerID3(pat);
            tagger.writeMP3(folder, NewName, extension);
        }*/

        public void reCreateLIBConformMP3(bool doIT)
        {
            //if(doIT) fi.MoveTo(folder + NewName+".mp3");

        }

    }
}
