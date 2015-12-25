using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IVirtualFileProvider
    {
        string[] RootNames { get; }
        Dictionary<int, IVirtualFile> this[string name] { get; }
        void Init(string[] overrideRootnames, int[] subRoots);
    }
}
