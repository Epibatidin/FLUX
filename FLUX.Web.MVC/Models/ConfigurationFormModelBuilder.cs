using System.Collections.Generic;
using DataAccess.Base.Config;
using FLUX.Interfaces.Web;

namespace FLUX.Web.MVC.Models
{
    public class ConfigurationFormModelBuilder : IConfigurationFormModelBuilder
    {
        private readonly IVirtualFileAccessorSectionGroupProvider _configProvider;

        public ConfigurationFormModelBuilder(IVirtualFileAccessorSectionGroupProvider configProvider)
        {
            _configProvider = configProvider;
        }


        public IEnumerable<string> AvailableProviders()
        {
            //_configProvider.VirtualFileAccessorConfig;

            return null;
        }
        

    }

    
}