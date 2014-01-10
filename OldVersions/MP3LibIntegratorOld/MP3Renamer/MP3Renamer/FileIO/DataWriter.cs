//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using MP3Renamer.Config;
//using MP3Renamer.DataContainer.EntityInterfaces;
//using MP3Renamer.Models;

//namespace MP3Renamer.FileIO
//{
//    public class DataWriter
//    {
//        private int CopyMode;

//        public DataWriter(int CopyMode)
//        {
//            this.CopyMode = CopyMode;
//        }

//        private enum WriteType
//        {
//            WriteAsRead = 1,
//            WriteFirstLetterUpper = 2,
//            AllUpper = 3
//        }

//        private enum knownFormats : byte
//        {
//            Root = 0,
//            Artist = 1,
//            Year = 2,
//            Album = 3,
//            CDCount = 4,
//            TrackNumber = 5,
//            SongName = 6
//        }       

//        /// 0 base folder
//        /// 1 Artist Name
//        /// 2 Album erscheinungsjahr
//        /// 3 Album NAme
//        /// 4 CD Nummer
//        /// 5 Tracknummer
//        /// 6 Trackname
//        /// 
//        private string FormatString = "{0}\\{1}\\{2} - {3}\\CD{4}\\[{5}] - {6} - {1}";
//        private string[] FormatStrings = new string[]
//        {
//            "{0}{1}\\",  // root 
//            "{2} - {3}\\", // subroot
//            "CD{4}\\", // extra cd 
//            "[{5}] - {6} - {1}" // files
//        };

//        private string getRootFolderByCopyMode()
//        {
//            switch (CopyMode)
//            {
//                case 1:
//                    return Configuration.TargetFolder;
//                case 2:
//                    return Configuration.MoqFolder;
//            }
//            return "";
//        }


        

//        public void WriteToFileSystem()
//        {
//            FileManager.Get.ResetPos();

//            foreach (IRoot root in FileManager.Get.Roots())
//            {
//                foreach (ISubRoot subRoot in root.SubRoots)
//                {
//                    foreach (ILeaf leaf in subRoot.Leafs)
//                    {
//                        WriteFolderStruct(root, subRoot, leaf);
//                    }
//                }
//            }
//        }

        
//        private void WriteFolderStruct(IRoot root, ISubRoot subRoot, ILeaf leaf)
//        {
//            List<string> formatstrings = FormatString.Split('\\').ToList();

//            string[] info = fillCurrentInfo(root, subRoot, leaf);
            
//            // bzw da das nicht geht 
//            // wird das stück formatstring das diese lehren zahlen enthält nicht mit ein gefügt 
//            // das alles noch in ordner ändern wird fies 
//            DirectoryInfo Folder = null;
//            bool ContainsValue = false;

//            // das alles soll nur für die ordner geschehen 
//            for(int i = 0; i< formatstrings.Count -1 ; i++)
//            {
//                foreach(char c in formatstrings[i])
//                {
//                    // such die zahlen im formatstring
//                    if( 47 < c &&  c < 58) // wenn es ne zahl ist , ich brauch nur einstellige suchen 
//                    {
//                        // zum int parsen schauen was in dem array an information enthalten ist 
//                        // format entfernen oder nicht .... 
//                        int pos = c - 48; // an manchen stellen hatten sie einfach keine lust mehr
//                        if(pos < info.Length)
//                        {
//                            // kontrolliere alle werte die zu diesen zahlen gehören 
//                            if(!String.IsNullOrWhiteSpace(info[pos]))
//                            {
//                                ContainsValue = true;
//                                break;
//                            }
//                        }
//                    }                
//                }
//                if(!ContainsValue)
//                {
//                    // wer fehlt fliegt 
//                    formatstrings.RemoveAt(i);
//                    i--;
//                    continue;
//                }
//                ContainsValue = false;
               
//                // hier wird das getan was notwendiog ist ?! 
//                // formatstück nehmen info anwenden 
//                string currentSubFolder = ApplyWriteType(String.Format(formatstrings[i], info));

//                if (Folder == null)
//                {
//                    Folder = new DirectoryInfo(currentSubFolder);
//                }
//                else
//                {
//                    Folder = Folder.CreateSubdirectory(currentSubFolder);
//                }
//                Folder.Create();
//            }
            
//            // jetzt "nur" noch die Dateien aber das ja egal 
//            FileInfo fi = new FileInfo(leaf.FullFilePath);

//            string folder = Folder.FullName;
//            string extension = fi.Extension;
//            string fileName = ApplyWriteType(String.Format(formatstrings[formatstrings.Count - 1], info));

//            string FullTargetName = folder + "\\" + fileName + extension;

//            Copy(FullTargetName, leaf.FullFilePath);

//            //fi.CopyTo(FullName, true);        
//        }






//        private WriteType myWriteType = WriteType.WriteFirstLetterUpper;
//        private string ApplyWriteType(string input)
//        {
//            switch (myWriteType)
//            {
//                case WriteType.WriteAsRead:
//                    return input;
//                case WriteType.WriteFirstLetterUpper:
//                    return FirstLetterToUpper(input);
//                case WriteType.AllUpper:
//                    return input.ToUpper();
//            }
//            return "";
//        }

//        [System.Diagnostics.DebuggerStepThrough]
//        private string FirstLetterToUpper(string input)
//        {
//            StringBuilder b = new StringBuilder(input);
//            bool NextWord = true;

//            for (int i = 0; i < b.Length; i++)
//            {
//                if (b[i] == ' ')
//                {
//                    NextWord = true;
//                    continue;
//                }

//                if (NextWord)
//                {
//                    b[i] = b[i].ToString().ToUpper()[0];
//                    NextWord = false;
//                }
//            }
//            return b.ToString();
//        }


        
//        //public int TotalWorkAmount{get ; private set;}
//        //public int WorkDone { get; private set; }
                
       



//        ////-----------------------------------------------------------------------------------------------------------------------
//        //public void WriteToFileSystem()
//        ////-----------------------------------------------------------------------------------------------------------------------
//        //{
//        //    FileManager.Get.ResetPos();
            
//        //    string FullName = "";
//        //    DirectoryInfo currentRootDir = null;
//        //    DirectoryInfo currentSubRootDir = null;
//        //    foreach (var Root in FileManager.Get.Roots())
//        //    {
//        //        // create root folder from info 
//        //        currentRootDir = writeRootFolder(Root);
//        //        if (currentRootDir == null) break;
//        //        foreach (var SubRoot in Root.SubRoots)
//        //        {
//        //            // create sub root folder 
//        //            currentSubRootDir = writeSubRootFolder(SubRoot, currentRootDir);    
//        //            foreach (var Leaf in SubRoot.Leafs)
//        //            {
//        //                if (SubRoot.CDCount > 1)
//        //                {
//        //                    currentSubRootDir = writeCDExtraFolder(Leaf.Count, currentSubRootDir);
//        //                }
//        //                // eventuell cd ordner erzeugen  
//        //                // write files
//        //                FullName = AssambleInfo(Root, SubRoot, Leaf);
//        //                Write();
//        //                currentWorkItem++;
//        //            }
//        //        }
//        //    }
//        //}


//        //private List<string> currentInfo = null; //= new List<string>() { RootFolder }; 
        
//        ////-----------------------------------------------------------------------------------------------------------------------
//        //private DirectoryInfo writeRootFolder(IRoot Root)
//        ////-----------------------------------------------------------------------------------------------------------------------
//        //{
//        //    // erst pfad zusammenbauen 
//        //    // dann schauen ob er da ist 
//        //    // sonst erzeugen
                      
//        //    currentInfo[(byte)knownFormats.Artist] = Root.Name;

//        //    string rootPath = ApplyWriteType(String.Format(FormatStrings[0],currentInfo));

//        //    DirectoryInfo diRoot = new DirectoryInfo(rootPath);

//        //    if (!diRoot.Exists)
//        //    {
//        //        diRoot.Create();
//        //    }
//        //    return diRoot;
//        //}

        
//        ////-----------------------------------------------------------------------------------------------------------------------
//        //private DirectoryInfo writeSubRootFolder(ISubRoot SubRoot, DirectoryInfo RootDir)
//        ////-----------------------------------------------------------------------------------------------------------------------
//        //{
//        //    currentInfo[(byte)knownFormats.Year] = SubRoot.Year > 0 ? SubRoot.Year.ToString() : "";
//        //    currentInfo[(byte)knownFormats.Artist] = SubRoot.Name;

//        //    string subRootFolder = ApplyWriteType(String.Format(FormatStrings[1], currentInfo));

//        //    return RootDir.CreateSubdirectory(subRootFolder);
//        //}


//        //private DirectoryInfo writeCDExtraFolder(byte CurrentCD, DirectoryInfo currentSubRootDir)
//        //{
//        //    currentInfo[(byte)knownFormats.CDCount] = CurrentCD.ToString();
 
//        //    string cdFolder = String.Format(FormatStrings[2], currentInfo);

//        //    return currentSubRootDir;
//        //}

//        //private string GenerateFullFileName(ILeaf leaf, ISubRoot subRoot)
//        //{

//        //    //   TrackNumber = 5,
//        //    //SongName = 6,
//        //    //Extension = 7





//        //    return "";
//        //}


        
//        ////-----------------------------------------------------------------------------------------------------------------------
//        //private string AssambleInfo(IRoot Root, ISubRoot SubRoot, ILeaf Leaf)
//        ////-----------------------------------------------------------------------------------------------------------------------
//        //{
//        //    string[] Info = new string[8];
//        //    Info[0] = Configuration.TargetFolder;
//        //    Info[1] = Root.Name;
//        //    Info[2] = SubRoot.Year > 0 ? SubRoot.Year.ToString() : "";
//        //    Info[3] = SubRoot.Name;
//        //    Info[4] = Leaf.Count > 0 ? Leaf.Count.ToString() : "";
//        //    string tracknr = "";
                        
//        //    if(Leaf.Number > 0)
//        //    {
//        //        tracknr = Leaf.Number.ToString();
//        //        if (Leaf.Number < 10)
//        //        {
//        //            tracknr = "0" + tracknr;
//        //        }
//        //    }
//        //    Info[5] = tracknr;
//        //    Info[6] = Leaf.Name;
//        //    Info[7] = "mp3";

//        //    string formatString = FormatStrings[0] + FormatStrings[1];

//        //    if (SubRoot.CDCount > 1)
//        //    {
//        //        formatString += FormatStrings[2];
//        //    }
//        //    formatString += FormatStrings[3];

//        //    List<string> Folder = new List<string>();
//        //    string formatted = "";

//        //    for (int i = 0; i < FormatStrings.Length -1; i++)
//        //    {
//        //        formatted = String.Format(FormatStrings[i], Info);
//        //        Folder.Add(formatted);
//        //    }
            
//        //    return String.Format(formatString, Info);                      
//        //}

//        //private void Write()
//        //{





//        //}


//        ////-----------------------------------------------------------------------------------------------------------------------
//        //private void createFolderStruct(string[] Dirs)
//        ////-----------------------------------------------------------------------------------------------------------------------
//        //{


//        //}


//        //private int currentWorkItem = 0;
//        //internal int currentwasauchimmer()
//        //{
//        //    return currentWorkItem;

//        //}
//    }
//} 