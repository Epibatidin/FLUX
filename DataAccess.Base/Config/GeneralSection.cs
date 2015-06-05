using System.Configuration;

namespace DataAccess.Base.Config
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