using System.Configuration;

namespace VirtualFileProvider.Config
{
    public class GeneralSection : ConfigurationSection
    {
        [ConfigurationProperty("Active", IsRequired = true)]
        public string Active
        {
            get { return (string)this["Active"]; }
        }


     

    }
}