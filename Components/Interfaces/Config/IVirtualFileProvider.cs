using System.Collections.Generic;
using Interfaces.VirtualFile;

namespace Interfaces.Config
{
    public interface IVirtualFileProvider
    {
        string[] RootNames { get; }
        Dictionary<int, IVirtualFile> this[string name] { get; }
        void Init(string[] overrideRootnames, int[] subRoots);
    }
}
