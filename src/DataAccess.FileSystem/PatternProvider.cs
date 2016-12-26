using DataAccess.Interfaces;
using System;

namespace DataAccess.FileSystem
{
    public class PatternProvider : IPatternProvider
    {
        public string GroupBy(IExtractionValueFacade facade, int lvl)
        {
            return null;

        }

        public FileWritePatternPartHolder LevelBuilder(IExtractionValueFacade facade, int lvl)
        {
            return null;
        }
    }
}
