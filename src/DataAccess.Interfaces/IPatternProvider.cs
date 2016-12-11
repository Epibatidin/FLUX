using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IPatternProvider
    {
        string[] ResolvePathParts(IDictionary<string,string> values);
    }
}
