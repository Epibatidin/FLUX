using System.Configuration;
using ConfigurationExtensions;
using ConfigurationExtensions.Interfaces;

namespace VirtualFileProvider.Directory
{
    public class FolderSource : ConfigurationElement, IKeyedElement
    {
        [ConfigurationProperty("Name", IsRequired = true, IsKey = true)]
        public string Key
        {
            get
            {
                return (string)base["Name"];
            }
        }


        [ConfigurationProperty("SubFolder", IsRequired = true)]
        public string SubFolder
        {
            get
            {
                return (string)base["SubFolder"];
            }
        }
    }
}
