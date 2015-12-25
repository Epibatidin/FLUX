using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IVirtualFileRootConfiguration
    {
        string Root { get; set; }

        IEnumerable<string> Keys { get; }
    }
}
