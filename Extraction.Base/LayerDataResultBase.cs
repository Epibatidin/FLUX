using System.IO;
using DataAccess.Interfaces;

namespace Extraction.Base
{
    public class LayerDataResultBase : IVirtualFile
    {
        public LayerDataResultBase(int id, FileInfo fileData)
        {
            ID = id;
            Name = fileData.Name;
            VirtualPath = fileData.FullName;
        }

        public int ID { get; private set; }

        public string Name { get; private set; }      

        public string VirtualPath {get;private set;}

        public Stream Open()
        {
            return new FileStream(VirtualPath, FileMode.Open, FileAccess.Read);
        }
    }
}
