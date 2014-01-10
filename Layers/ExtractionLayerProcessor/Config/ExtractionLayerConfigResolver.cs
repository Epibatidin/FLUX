
using ConfigurationExtensions;
using ConfigurationExtensions.Interfaces;

namespace ExtractionLayerProcessor.Config
{
    public class ExtractionLayerConfigResolver
    {
        private static readonly SectionGroupSingletonHelper<ExtractionLayerConfig> Helper = new SectionGroupSingletonHelper<ExtractionLayerConfig>("ExtractionLayer");
        
        public void Reset()
        {
            Helper.Reset();
        }

        public ExtractionLayerConfig Get(IConfigurationLocator locator)
        {
            return Helper.Get(locator.Locate);
        }

    }
}
