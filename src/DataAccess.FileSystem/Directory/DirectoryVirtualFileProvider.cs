using System.Collections.Generic;
using System.IO;
using DataAccess.Interfaces;

namespace DataAccess.FileSystem.Directory
{
    public class DirectoryVirtualFileBuilder : IVirtualFileBuilder
    {
        public Dictionary<int, IVirtualFile> BuildVirtualFiles(IVirtualDirectory root, string name, int[] subRoots)
        {
            // read directory by name 
            // dir == artist 
            var dir = root.GetDirectory(name);
            Dictionary<int, IVirtualFile> result = new Dictionary<int, IVirtualFile>();
            // get alben 
            // iteriere erst über die dirs 
            // wähle die deren position in subroots enthalten ist 
            int pos = -1;
            int arrayPos = 0;

            foreach (var subdir in dir.GetDirectories())
            {
                pos++;
                if (subRoots != null)
                {
                    if (arrayPos < subRoots.Length)
                    {
                        if (pos == subRoots[arrayPos])
                        {
                            arrayPos++;
                        }
                        else
                            continue;
                    }
                    else
                        continue;

                }
                foreach (var virtualFile in subdir.GetFiles("*.mp3", true, c => (pos + 1) * 1000 + c))
                {
                    result.Add(virtualFile.ID, virtualFile);
                }
            }

            return result;
        }
    }
}
