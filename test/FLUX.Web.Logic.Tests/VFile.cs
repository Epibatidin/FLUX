using DataAccess.Interfaces;

namespace FLUX.Web.Logic.Tests
{
    public class VFile : IVirtualFile
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string VirtualPath { get; set; }
    }
}
