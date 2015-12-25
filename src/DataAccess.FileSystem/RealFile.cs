using System.IO;
using DataAccess.Interfaces;

namespace DataAccess.FileSystem
{
    public class RealFile : IVirtualFile
    {
        private readonly FileInfo _file;

        public RealFile(FileInfo file)
        {
            _file = file;
            ID = _file.GetHashCode();
        }

        public RealFile(FileInfo file, int id)
        {
            _file = file;
            ID = id;
        }

        public int ID { get; }

        public string Name => _file.Name;

        public string VirtualPath => _file.FullName;

        public Stream Open()
        {
            return _file.OpenRead();
        }
    }
}
