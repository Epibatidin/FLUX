using DataAccess.Interfaces;
using DynamicLoading;

namespace Extraction.Base.Config
{
    public class ExtractionLayerConfigItem :  IKeyedElement, IDynamicLoadableExtensionConfiguration
    {
        public string Name { get; set; }
        public string ExtractorType { get; set; }
        public bool Active { get; set; } = true;

        public string Key => Name;
        public string SetionName => Name;
        public string Type => ExtractorType;
    }
}
