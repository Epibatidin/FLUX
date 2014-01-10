using System.Collections.Generic;
using System.Configuration;
using Interfaces;
using Interfaces.VirtualFile;

namespace AbstractDataExtraction
{
    public interface IDataExtractionLayer
    {
        void Configure(ConfigurationSection config);
        void InitData(int constPathLength, Dictionary<int, IVirtualFile> _dirtyData);
        void SetUpdater(UpdateObject uo);
        void Execute();

    }
}
