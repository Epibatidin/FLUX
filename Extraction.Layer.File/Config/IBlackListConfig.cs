using System.Collections.Generic;

namespace Extraction.Layer.File.Config
{
    public interface IBlackListConfig
    {
        HashSet<string> BlackList { get; }

        bool RepairCurses { get; }
    }
}
