//using System.Configuration;
//using System.Xml;
//using Extension.Configuration;
//using Extraction.Interfaces.Config;

//namespace Extraction.Base.Config
//{
//    public class ExtractionLayerConfigItem : ConfigurationElement, IKeyedElement , IExtractionLayerConfigItem
//    {
//        [ConfigurationProperty("Name", IsRequired = true, IsKey = true)]
//        public string Key
//        {
//            get
//            {
//                return (string)base["Name"];
//            }
//        }

//        [ConfigurationProperty("ExtractorType", IsRequired = true)]
//        public string ExtractorType
//        {
//            get
//            {
//                return (string)base["ExtractorType"];
//            }
//        }

//        [ConfigurationProperty("active")]
//        public bool IsActive
//        {
//            get
//            {
//                return (bool)base["active"];
//            }
//        }

//        public XmlNode SectionData { get; private set; }

//        protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
//        {
//            XmlDocument doc = new XmlDocument();
//            SectionData = doc.ReadNode(reader);
//            return SectionData != null;
//        }
//    }
//}
