using DataAccess.Interfaces;
using System.Collections.Generic;

namespace DataAccess.FileSystem
{
    public interface IPatternProvider
    {
        IList<string> FormattedLevelValue(IExtractionValueFacade facade);
    }
}
