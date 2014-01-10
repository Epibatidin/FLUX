using System.IO;

namespace MP3Renamer.FileIO.WriteExecution
{
    public class ZeroCopy : CopyHelperBase
    {


        public override void Copy(string From, string To)
        {
            
        }

        public override string SuperRootFolder
        {
            get 
            {
                return "_";
            }
        }


        protected override DirectoryInfo ProtectedSwitchOrCreateDir(DirectoryInfo baseDir, string Path)
        {
            return new DirectoryInfo(baseDir + "__" + Path);         
        }
    }
}