using DataAccess.Interfaces;

namespace DataAccess.FileSystem.Config
{
    public class FolderSource  : IKeyedElement
    {
        public string Name { get; set; }

        public string SubFolder { get; set; }

        public string Key => Name;
    }
}
