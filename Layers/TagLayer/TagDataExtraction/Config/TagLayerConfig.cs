using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace TagDataExtraction.Config
{
    public class TagLayerConfig : ConfigurationSection
    {
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
