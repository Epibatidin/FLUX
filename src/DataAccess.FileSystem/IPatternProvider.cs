using DataAccess.Interfaces;

namespace DataAccess.FileSystem
{
    public interface IPatternProvider
    {
        string FormattedLevelValue(IExtractionValueFacade facade, int lvl);
    }
}
