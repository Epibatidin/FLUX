using System.Configuration;
using FileStructureDataExtraction.Config;

namespace ExtractionLayerProcessor.Config
{
    public class ExtractionLayerConfig : ConfigurationSectionGroup
    {
        [ConfigurationProperty("ASync")]
        public bool ASync { get; set; }


        //public FileLayerConfig FileLayer
        //{
        //    get
        //    {
        //        return Sections["FileLayer"] as FileLayerConfig;
        //    }
        //}
        

    }
}
