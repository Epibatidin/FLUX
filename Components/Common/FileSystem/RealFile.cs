using System.IO;
using Interfaces.VirtualFile;

namespace Common.FileSystem
{
    public class RealFile : IVirtualFile
    {
        private FileInfo _file;

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

        public int ID { get; private set; }

        public string Name
        {
            get { return _file.Name; }
        }

        public string VirtualPath
        {
            get { return _file.FullName; }
        }

        public Stream Open()
        {
            return _file.OpenRead();
        }
    }
}
