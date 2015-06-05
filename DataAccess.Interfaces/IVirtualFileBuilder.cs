using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IVirtualFileBuilder
    {
        Dictionary<int, IVirtualFile> BuildVirtualFiles(IVirtualDirectory root, string name, int[] subRoots);
    }
}
