using DataAccess.Base.Config;
using Extraction.Base.Config;
using FLUX.Interfaces.Configuration;

namespace FLUX.Configuration.Config
{
    public interface IConfigurationFactory
    {
        ConfigurationData Build();
    }

    public class ConfigurationFactory : IConfigurationFactory
    {
        private readonly IConfigurationLocator _locator;
        
        public ConfigurationFactory(IConfigurationLocator locator)
        {
            _locator = locator;
        }

        public ConfigurationData Build()
        {
            var data = new ConfigurationData();
            data.ExtractionLayerConfig = _locator.Locate("Layer").GetSection("ExtractionLayer") as ExtractionLayerConfig;
            data.VirtualFileAccessorConfig = _locator.Locate("VirtualFileProvider").GetSectionGroup("VirtualFileAccessor") as VirtualFileAccessorSectionGroup;

            return data;
        }
    }
}