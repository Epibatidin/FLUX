using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;

namespace DataAccess.FileSystem.Config
{
    public class DirectorySourcesCollection : IVirtualFileRootConfiguration
    {
        public string ID { get; set; }

        public string Root { get; set; }

        public List<FolderSource> Folder { get; set; }

        public IEnumerable<string> Keys => Folder.Select(c => c.Key);
    }
}
