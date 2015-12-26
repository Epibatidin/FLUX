using System;
using System.Linq;
using DataAccess.Interfaces;
using FLUX.DomainObjects;
using FLUX.Interfaces.Web;
using Microsoft.AspNet.Mvc;
using Facade.MVC;

namespace FLUX.Web.Logic
{
    public class ConfigurationFormModelBuilder : IConfigurationFormModelBuilder
    {
        private readonly IVirtualFileConfigurationReader _configProvider;
        private readonly IPostbackHelper _postbackHelper;
        private readonly IModelBinderFacade _modelBinder;

        public ConfigurationFormModelBuilder(IVirtualFileConfigurationReader configProvider, IPostbackHelper postbackHelper, IModelBinderFacade modelBinder)
        {
            _configProvider = configProvider;
            _postbackHelper = postbackHelper;
            _modelBinder = modelBinder;
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
            if (_postbackHelper.IsPostback(controller.Request))
                _modelBinder.TryUpdateModel(formModel, controller);
        }

        public void Process(ConfigurationFormModel formModel)
        {
            var providers = formModel.VirtualFileProvider;

            // select grp by providername 
            ProviderGroupDo activeGrp = null;
            foreach (var providerGroup in providers.ProviderNames)
            {
                foreach (var providerName in providerGroup.VirtualFileProviderNames)
                {
                    if(!providers.IsSelected(providerName)) continue;

                    activeGrp = providerGroup;
                    break;
                }
            }

            if(activeGrp == null)
                throw new NotSupportedException(string.Format( "No providers found for {0}", providers.CurrentProviderName));

            //var files = _configProvider.GetVirtualFiles(providers.CurrentProviderName, activeGrp.ProviderKey);
        }
        
    }
}