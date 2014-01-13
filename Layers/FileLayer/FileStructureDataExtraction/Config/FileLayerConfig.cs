using System.Configuration;
using System.Xml;

namespace FileStructureDataExtraction.Config
{
    public class FileLayerConfig : ConfigurationSection
    {
        public FileLayerConfig(XmlReader reader)
        {
            DeserializeSection(reader);
        }

        //public void Init(XmlReader reader)
        //{
        //    DeserializeElement(reader, false);
        //}

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
