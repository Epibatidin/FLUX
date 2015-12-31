//using System.Configuration;
//using System.Xml;

//namespace Extraction.Layer.File.Config
//{
//    public class FileLayerConfig : ConfigurationSection
//    {
//        public FileLayerConfig(XmlReader reader)
//        {
//            DeserializeSection(reader);
//        }
        
//        [ConfigurationProperty("BlackList")]
//        public BlackListConfig BlackListConfig
//        {
//            get
//            {
//                return (BlackListConfig)base["BlackList"];
//            }
//        }

//        [ConfigurationProperty("WhiteList")]
//        public WhiteListConfig WhiteListConfig
//        {
//            get
//            {
//                return (WhiteListConfig)base["WhiteList"];
//            }
//        }        
//    }
//}
