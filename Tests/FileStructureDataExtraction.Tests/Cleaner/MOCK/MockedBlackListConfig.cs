using System.Collections.Generic;
using FileStructureDataExtraction.Config;
using GenericConfigSection;

namespace Cleaner.Tests.MOCK
{
    internal class MockedBlackListConfig : IBlackListConfig
    {

        public MockedBlackListConfig(string blackEntries, bool _repairCurses)
        {
            this.BlackList = Helper.CommaSeparatedStringToHashSet(blackEntries);
            this.RepairCurses = _repairCurses;
        }


        public HashSet<string> BlackList { get; private set; }

        public bool RepairCurses { get; private set; } 
    }
}