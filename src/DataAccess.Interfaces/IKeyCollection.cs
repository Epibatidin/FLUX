using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IKeyCollection
    {
        IEnumerable<string> Keys { get; }
    }
}