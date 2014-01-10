using System;

namespace ExtractionLayerProcessor.Config
{
    public interface IExtractionLayerConfigItem
    {
        string Key { get; }
        Type LayerType { get; }
        string ConfigSectionName { get; }
    }
}
