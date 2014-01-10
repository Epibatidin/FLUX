using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Interfaces.VirtualFile;

namespace VirtualFileProvider.Directory
{
    public class DirectoryVirtualFileProvider : AbstractVirtualFileProvider
    {
        protected override Dictionary<int, IVirtualFile> getDataByKey(string name, int[] subRoots)
        {
            // read directory by name 
            // dir == artist 
            var dir = _root.GetDirectory(name);
            Dictionary<int, IVirtualFile> _result = new Dictionary<int, IVirtualFile>();
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
                foreach (var virtualFile in subdir.GetFiles("*.mp3", SearchOption.AllDirectories, c => (pos+1)*1000 + c))
                {
                    _result.Add(virtualFile.ID, virtualFile);
                }
            }
            
            return _result;
        }
    }
}
