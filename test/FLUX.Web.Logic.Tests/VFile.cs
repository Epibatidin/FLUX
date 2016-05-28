using DataAccess.Interfaces;

namespace FLUX.Web.Logic.Tests
{
    public class VFile : IVirtualFile
    {
        public int ID { get; set; }
        public string Extension { get; set; }
        public string Name { get; set; }
        public string[] PathParts { get; set; }

        private string _vfPath;
        public string VirtualPath
        {
            get { return _vfPath; }
            set
            {
                PathParts = value.Split('\\');
                _vfPath = value;
            }
        }
    }
}
