using System.Configuration;

namespace FLUX.Interfaces.Configuration
{
    public interface IConfigurationLoader
    {
        TSectionGroup LoadSectionGroup<TSectionGroup>(string sectionGroupName) 
            where TSectionGroup : ConfigurationSectionGroup;

        TSection LoadSection<TSection>(string configFileName,string sectionName)
            where TSection : ConfigurationSection;
        
    
    }
}
