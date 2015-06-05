using System.Configuration;
using Extension.Configuration;

namespace DataAccess.FileSystem.Config
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
