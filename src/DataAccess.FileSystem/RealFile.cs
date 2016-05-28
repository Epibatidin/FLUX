using DataAccess.Interfaces;

namespace DataAccess.FileSystem
{
    public class RealFile : IVirtualFile
    {
        public int ID { get; set; }
        public string Extension { get; set; }

        public string Name { get; set; }
        public string[] PathParts { get; set; }
    }
}
