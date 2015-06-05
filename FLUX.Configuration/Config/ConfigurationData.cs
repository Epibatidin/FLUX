using DataAccess.Base.Config;
using Extraction.Base.Config;

namespace FLUX.Configuration.Config
{
    public class ConfigurationData : IExtractionLayerConfigurationProvider , IVirtualFileAccessorSectionGroupProvider
    {
        public ExtractionLayerConfig ExtractionLayerConfig { get; set; }

        public VirtualFileAccessorSectionGroup VirtualFileAccessorConfig { get; set; }
    }
}