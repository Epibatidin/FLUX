using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Interfaces.VirtualFile;

namespace Interfaces.FileSystem
{
    public interface IVirtualDirectory
    {
        string DirectoryName { get; }

        IVirtualFile GetFile(string name);

        IEnumerable<IVirtualFile> GetFiles();
        IEnumerable<IVirtualFile> GetFiles(string searchPattern);
        IEnumerable<IVirtualFile> GetFiles(string searchPattern, SearchOption searchOption);
        IEnumerable<IVirtualFile> GetFiles(string searchPattern, SearchOption searchOption, Func<int, int> idGenerator);
            
        IVirtualDirectory GetDirectory(string name);
        IEnumerable<IVirtualDirectory> GetDirectories();
        IEnumerable<IVirtualDirectory> GetDirectories(string searchPattern);
        IEnumerable<IVirtualDirectory> GetDirectories(string searchPattern, SearchOption searchOption);
    }
}
