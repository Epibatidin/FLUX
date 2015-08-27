using System;
using DataAccess.Interfaces;
using FLUX.DomainObjects;
using FLUX.Interfaces.Web;

namespace FLUX.Web.Logic
{
    public class ConfigurationFormModelBuilder : IConfigurationFormModelBuilder
    {
        private readonly IVirtualFileConfigurationReader _configProvider;

        public ConfigurationFormModelBuilder(IVirtualFileConfigurationReader configProvider)
        {
            _configProvider = configProvider;
        }
        
        public ConfigurationFormModel BuildFormModel()
        {
            var result = new ConfigurationFormModel();

            var configuration = _configProvider.ReadToDO();

            var formModel = new AvailableVirtualFileProviderFormModel();
            formModel.CurrentProviderName = configuration.CurrentProviderName;
            formModel.ProviderNames = configuration.ProviderNames;

            result.VirtualFileProvider = formModel;
            //{
            //    IsSelectedEvaluator =
            //        (val, item) => string.Equals(val, item, StringComparison.InvariantCultureIgnoreCase),
            //    Value = configFromProvider.CurrentProviderName,
            //    Items = configFromProvider.ProviderNames

            //};
            return result;
        }
    }
}