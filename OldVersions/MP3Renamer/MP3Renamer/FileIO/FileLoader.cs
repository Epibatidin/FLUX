using System.Collections.Generic;
using System.IO;

using MP3Renamer.Config;


namespace MP3Renamer.FileIO
{       
    public class FileLoader
    {

        #region Member
        public string InitalPath { get; private set; }

        List<FileInfo> Files = null;

        #endregion

        #region Ctor
        /// <summary>
        /// erzeugt eine neue FileLoader Instanz mit dem InitalPfad 
        /// D:\Testbench
        /// </summary>
        public FileLoader()
        {
            InitalPath = Configuration.SourceFolder;    
        }

        /// <summary>
        /// erzeugt eine neue FileLoader Instanz mit spezifiziertem InitalPfad
        /// </summary>
        /// <param name="InitialPath">
        /// Pfad in dem die Dateien zu suchen sind
        /// </param>
        public FileLoader(string initialPath)
        {
            InitalPath = initialPath;
        }

        #endregion


        #region Functions      
        /// <summary>
        /// Lädt rekursiv alle Dateien mit unterstützter Dateiendung aus dem InitalPfad
        /// </summary>
        /// <param name="folderPath">
        /// Pfad in dem unterstützte Dateien zu suchen sind
        /// </param>
        private void LoadAllFilesFromFolder(string folderPath)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);
            if (di.Exists)
            {
                if (Files == null) Files = new List<FileInfo>();

                var fInfos = di.GetFiles("*.*", SearchOption.AllDirectories);
                foreach (var fInfo in fInfos)
                {
                    if (ExtensionHelper.IsSupportedFile(fInfo))
                    {
                        Files.Add(fInfo);
                    }
                }
            }
        }
        
        public List<FileInfo> FileList
        {
            get
            {
                if (Files == null)
                {
                    LoadAllFilesFromFolder(InitalPath);
                }
                return Files;
            }
        }
        #endregion

    }
}