using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IVirtualFileRootConfiguration
    {
        string ID { get; set; }
        
        IEnumerable<string> Keys { get; }
    }
}
