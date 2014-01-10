using System.Collections.Generic;

namespace FileStructureDataExtraction.Config
{
    public interface IBlackListConfig
    {
        HashSet<string> BlackList { get; }
        bool RepairCurses { get; }
    }
}
