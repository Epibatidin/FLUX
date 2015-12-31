using System.Collections.Generic;
using DynamicLoading;

namespace DataAccess.Interfaces
{
    public interface IVirtualFileRootConfiguration : ISectionNameHolder
    {
        IEnumerable<string> Keys { get; }
    }
}
