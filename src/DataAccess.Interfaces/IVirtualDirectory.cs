using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IVirtualDirectory
    {
        string DirectoryName { get; }

        string GetFile(string name);

        IEnumerable<string> GetFiles();
        IEnumerable<string> GetFiles(string searchPattern);
        IEnumerable<string> GetFiles(string searchPattern, bool deepSearch);

        IVirtualDirectory GetDirectory(string name);
        IEnumerable<IVirtualDirectory> GetDirectories();
        IEnumerable<IVirtualDirectory> GetDirectories(string searchPattern);
    }
}
