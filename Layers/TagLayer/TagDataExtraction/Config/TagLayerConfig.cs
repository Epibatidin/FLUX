using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;

namespace TagDataExtraction.Config
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
