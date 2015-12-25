using DataAccess.Interfaces;
using FLUX.DomainObjects;
using FLUX.Interfaces.Web;
using Microsoft.AspNet.Mvc;


namespace FLUX.Web.Logic
{
    public class ConfigurationFormModelBuilder : IConfigurationFormModelBuilder
    {
        private readonly IVirtualFileConfigurationReader _configProvider;

        public ConfigurationFormModelBuilder(IVirtualFileConfigurationReader configProvider)
        {
            _configProvider = configProvider;
        }
        
        public ConfigurationFormModel Build()
        {
            var result = new ConfigurationFormModel();

            var configuration = _configProvider.ReadToDO();

            var formModel = new AvailableVirtualFileProviderFormModel();
            formModel.CurrentProviderName = configuration.CurrentProviderName;
            formModel.ProviderNames = configuration.ProviderNames;

            result.VirtualFileProvider = formModel;

            return result;
        }

        public void Update(ConfigurationFormModel formModel, Controller controller)
        {
        }
        
    }
}