using System;
using System.Xml;

namespace ExtractionLayerProcessor.Config
{
    public interface IExtractionLayerConfigItem
    {
        bool IsActive { get; }
        string Key { get; }
        string ExtractorType { get; }
        XmlNode SectionData { get; }
    }
}
