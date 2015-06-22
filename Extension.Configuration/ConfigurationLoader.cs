using System.Configuration;

namespace Extension.Configuration
{
    public class ConfigurationLoader : IConfigurationLoader
    {
        private readonly IConfigurationLocator _configLocator;

        public ConfigurationLoader(IConfigurationLocator configLocator)
        {
            _configLocator = configLocator;
        }
        
        public TSectionGroup LoadSectionGroup<TSectionGroup>(string configFileName, string sectionGroupName) where TSectionGroup : ConfigurationSectionGroup
        {
            var config = _configLocator.Locate(configFileName);
            return Assert<TSectionGroup>(config.GetSectionGroup(sectionGroupName), sectionGroupName);
        }

        public TSection LoadSection<TSection>(string configFileName, string sectionName) where TSection : ConfigurationSection
        {
            var config = _configLocator.Locate(configFileName);
            return Assert<TSection>(config.GetSection(sectionName), sectionName);
        }

        private TConfig Assert<TConfig>(object o, string name) where TConfig : class
        {
            if(o == null)
                throw new ConfigurationErrorsException("Configuration Element " + name + " was null");

            var oAsConfig = o as TConfig;
            if(oAsConfig == null)
                throw new ConfigurationErrorsException("Configuration Element " + name + " is not of Type " + typeof(TConfig));

            return oAsConfig;
        }
    }
}
