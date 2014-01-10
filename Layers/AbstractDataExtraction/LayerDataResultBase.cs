using System.IO;
using Interfaces;
using Interfaces.VirtualFile;

namespace AbstractDataExtraction
{
    public class LayerDataResultBase : IVirtualFile
    {
        public LayerDataResultBase(int id, FileInfo _fileData)
        {
            ID = id;
            Name = _fileData.Name;
            VirtualPath = _fileData.FullName;
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
