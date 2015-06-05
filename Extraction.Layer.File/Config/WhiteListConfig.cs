using System.Collections.Generic;
using System.Configuration;
using Extension.Configuration;

namespace Extraction.Layer.File.Config
{
    public class WhiteListConfig : ConfigurationElement , IWhiteListConfig
    {
        private HashSet<string> _whitelist;

        public HashSet<string> WhiteList
        {
            get
            {
                if (_whitelist == null)
                {
                    _whitelist = ConfigHelper.CommaSeparatedStringToHashSet(items);
                }
                return _whitelist;
            }
        }

        [ConfigurationProperty("Items", IsRequired = true)]
        private string items
        {
            get
            {
                return (string)base["Items"];
            }
        }
    }
}
