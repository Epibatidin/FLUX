using System.Configuration;
using ConfigurationExtensions;
using FileStructureDataExtraction.Config;

namespace ExtractionLayerProcessor.Config
{
    public class ExtractionLayerConfig : ConfigurationSection
    {
        [ConfigurationProperty("ASync")]
        public bool ASync
        {
            get
            {
                return (bool)base["ASync"];
            }
        }

        [ConfigurationProperty("LayerCollection")]
        [ConfigurationCollection(typeof(ExtractionLayerConfigItem), AddItemName = "Layer")]
        public GenericElementCollection<ExtractionLayerConfigItem> LayerCollection
        {
            get
            {
                return base["LayerCollection"] as GenericElementCollection<ExtractionLayerConfigItem>;
            }
        }
    }
}
