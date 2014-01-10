using System.Collections.Generic;
using FileStructureDataExtraction.Config;

namespace Cleaner.Tests.MOCK
{
    internal class MockedWhiteListConfig  : IWhiteListConfig
    {
        public MockedWhiteListConfig(string whiteEntries)
        {
            this.WhiteList = GenericConfigSection.Helper.CommaSeparatedStringToHashSet(whiteEntries);
        }

        public HashSet<string> WhiteList { get; private set; }
    }
}
