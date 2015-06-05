using System.Configuration;
using System.Xml;

namespace Extraction.Layer.Tag.Config
{
    public class TagLayerConfig : ConfigurationSection
    {
        public TagLayerConfig(XmlReader reader)
        {
            DeserializeSection(reader);
        }

        //[ConfigurationProperty("IgnorePrivateData", IsRequired = false, DefaultValue = true)]
        public bool IgnorePrivateData
        {
            get
            {
                return true;
            }
        }

    }
}
