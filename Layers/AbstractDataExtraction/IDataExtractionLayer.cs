using System.Collections.Generic;
using System.Xml;
using Interfaces.VirtualFile;

namespace AbstractDataExtraction
{
    public interface IDataExtractionLayer
    {
        void Configure(XmlNode config);
        void InitData(int constPathLength, Dictionary<int, IVirtualFile> _dirtyData);
        void SetUpdater(UpdateObject uo);
        void Execute();

    }
}
