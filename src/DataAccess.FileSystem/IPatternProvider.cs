using DataAccess.Interfaces;

namespace DataAccess.FileSystem
{
    public interface IPatternProvider
    {
        string GroupBy(IExtractionValueFacade facade, int lvl);

        FileWritePatternPartHolder LevelBuilder(IExtractionValueFacade facade, int lvl);
    }
}
