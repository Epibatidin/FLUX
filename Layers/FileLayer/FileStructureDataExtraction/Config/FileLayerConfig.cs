using System.Configuration;

namespace FileStructureDataExtraction.Config
{
    public class FileLayerConfig : ConfigurationSection
    {
        [ConfigurationProperty("BlackList")]
        public BlackListConfig BlackListConfig
        {
            get
            {
                return (BlackListConfig)base["BlackList"];
            }
        }

        [ConfigurationProperty("WhiteList")]
        public WhiteListConfig WhiteListConfig
        {
            get
            {
                return (WhiteListConfig)base["WhiteList"];
            }
        }        
    }
}
