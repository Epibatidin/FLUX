//using System.Configuration;
//using Extension.Configuration;

//namespace Extraction.Base.Config
//{
//    public class ExtractionLayerConfig : ConfigurationSection
//    {
//        [ConfigurationProperty("ASync")]
//        public bool ASync
//        {
//            get
//            {
//                return (bool)base["ASync"];
//            }
//        }

//        [ConfigurationProperty("LayerCollection")]
//        [ConfigurationCollection(typeof(ExtractionLayerConfigItem), AddItemName = "Layer")]
//        public GenericElementCollection<ExtractionLayerConfigItem> LayerCollection
//        {
//            get
//            {
//                return base["LayerCollection"] as GenericElementCollection<ExtractionLayerConfigItem>;
//            }
//        }
//    }
//}
