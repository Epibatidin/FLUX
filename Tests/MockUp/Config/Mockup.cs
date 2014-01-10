using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace MockUp.Config
{
    
    public class Mockup : ConfigurationSection
    {       
        public static Mockup Create()
        {
            return (Mockup)ConfigurationManager.GetSection("Mockup");
        }


        [ConfigurationProperty("XMLFolder", IsRequired = true)]
        public string XMLFolder
        {
            get
            {
                return (string)base["XMLFolder"];
            }
        }

        [ConfigurationProperty("RawDataFolder", IsRequired = true)]
        public string RawDataFolder
        {
            get
            {
                return (string)base["RawDataFolder"];
            }
        }


        [ConfigurationProperty("OutActive", IsRequired = true)]
        public bool OutActive
        {
            get
            {
                return (bool)base["OutActive"];
            }
        }

    }
}
