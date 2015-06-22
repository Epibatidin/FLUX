using System.Configuration;

namespace Extension.Configuration
{
    public class ConfigurationHolderFactory
    {
        private readonly IConfigurationLoader _configLoader;

        public ConfigurationHolderFactory(IConfigurationLoader configLoader)
        {
            _configLoader = configLoader;
        } 

        public ConfigurationHolder<TConfig> Build<TConfig>(string configFileName, string sectionName) where TConfig : ConfigurationSection
        {
            var holder = new ConfigurationHolder<TConfig>();
            holder.Configuration = _configLoader.LoadSection<TConfig>(configFileName, sectionName);
            return holder;
        }


        public TConfigHolder BuildGroup<TConfigHolder,TConfig>(string configFileName, string sectionName) 
            where TConfigHolder : ConfigurationHolder<TConfig>, new() where TConfig : ConfigurationSectionGroup
        {
            var holder = new TConfigHolder();
            holder.Configuration = _configLoader.LoadSectionGroup<TConfig>(configFileName, sectionName);
            return holder;
        }
    }
}