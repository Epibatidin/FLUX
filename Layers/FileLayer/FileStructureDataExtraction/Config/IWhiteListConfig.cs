using System.Collections.Generic;

namespace FileStructureDataExtraction.Config
{
    public interface IWhiteListConfig
    {
        HashSet<string> WhiteList { get; }
    }
}
