using System.IO;

namespace Interfaces.VirtualFile
{
    public interface IVirtualFile
    {
        int ID { get; }

        string Name {get;}

        string VirtualPath {get;}

        Stream Open(); 
    }
}
