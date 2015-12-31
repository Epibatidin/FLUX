namespace Extraction.Interfaces.Config
{
    public interface IExtractionLayerConfigItem
    {
        bool IsActive { get; }
        string Key { get; }
        string ExtractorType { get; }
    }
}
