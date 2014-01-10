using System.Configuration;
using ConfigurationExtensions;
using ConfigurationExtensions.Interfaces;

namespace VirtualFileProvider.XML
{
    public class XMLSource : ConfigurationElement, IKeyedElement
    {
        [ConfigurationProperty("Name", IsRequired = true, IsKey = true)]
        public string Key
        {
            get
            {
                return (string)base["Name"];
            }
        }

        [ConfigurationProperty("XMLFolder", IsRequired = true)]
        public string XMLFolder
        {
            get
            {
                return (string)base["XMLFolder"];
            }
        }
    }
}
