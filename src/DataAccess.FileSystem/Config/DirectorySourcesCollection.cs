using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;

namespace DataAccess.FileSystem.Config
{
    public class DirectorySourcesCollection : IVirtualFileRootConfiguration
    {
        public string Root { get; set; }

        public List<FolderSource> Folder { get; set; }

        public IEnumerable<string> Keys => Folder.Select(c => c.Key);

        //public IVirtualFileProvider Create(string sourceKey)
        //{
        //    DirectoryVirtualFileProvider fp = null;

        //    var item = Item(sourceKey);
        //    if (item != null)
        //    {
        //        fp = new DirectoryVirtualFileProvider();
        //        string path = Path.Combine(Root, item.SubFolder);

        //        fp.Setup(new RealDirectory(new DirectoryInfo(path)));
        //    }
        //    return fp;
        //}
    }
}
