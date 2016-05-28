//using System.IO;
//using DataAccess.Interfaces;

//namespace Extraction.Base
//{
//    public class LayerDataResultBase : IVirtualFile
//    {
//        public LayerDataResultBase(int id, FileInfo fileData)
//        {
//            ID = id;
//            Name = fileData.Name;
//            VirtualPath = fileData.FullName;
//        }

//        public int ID { get; }

//        public string Name { get; }      

//        public string VirtualPath {get; }

//        public Stream Open()
//        {
//            return new FileStream(VirtualPath, FileMode.Open, FileAccess.Read);
//        }
//    }
//}
