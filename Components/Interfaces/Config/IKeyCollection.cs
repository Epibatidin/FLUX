using System.Collections.Generic;

namespace Interfaces.Config
{
    public interface IKeyCollection
    {
        IEnumerable<string> Keys { get; }
    }
}