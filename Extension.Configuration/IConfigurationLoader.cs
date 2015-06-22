using System.Configuration;

namespace Extension.Configuration
{
    public interface IConfigurationLoader
    {
        TSectionGroup LoadSectionGroup<TSectionGroup>(string configFileName ,string sectionGroupName) 
            where TSectionGroup : ConfigurationSectionGroup;

        TSection LoadSection<TSection>(string configFileName,string sectionName)
            where TSection : ConfigurationSection;
    }
}
