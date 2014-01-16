﻿using System.Collections.Generic;
using System.Xml;
using Interfaces.VirtualFile;

namespace AbstractDataExtraction
{
    public interface IDataExtractionLayer
    {
        void Configure(XmlNode config);
        void InitData(Dictionary<int, IVirtualFile> dirtyData);
        void SetUpdater(UpdateObject uo);
        void Execute();
    }
}
