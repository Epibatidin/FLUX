using System.Xml;

namespace Extraction.Interfaces.Config
{
    public interface IExtractionLayerConfigItem
    {
        bool IsActive { get; }
        string Key { get; }
        string ExtractorType { get; }
        XmlNode SectionData { get; }
    }
}
