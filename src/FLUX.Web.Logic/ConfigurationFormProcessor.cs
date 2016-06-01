using System;
using DataAccess.Interfaces;
using FLUX.DomainObjects;
using FLUX.Interfaces.Web;
using Microsoft.AspNetCore.Mvc;
using Facade.MVC;
using Facade.Session;
using FLUX.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FLUX.Web.Logic
{
    public class ConfigurationFormProcessor : IConfigurationFormProcessor
    {
        private readonly IVirtualFileConfigurationReader _configProvider;
        private readonly IPostbackHelper _postbackHelper;
        private readonly IModelBinderFacade _modelBinder;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IVirtualFilePeristentHelper _persistentHelper;

        public ConfigurationFormProcessor(IVirtualFileConfigurationReader configProvider, 
            IPostbackHelper postbackHelper, IModelBinderFacade modelBinder, IHttpContextAccessor httpContextAccessor,
            IVirtualFilePeristentHelper persistentHelper)
        {
            _configProvider = configProvider;
            _postbackHelper = postbackHelper;
            _modelBinder = modelBinder;
            _httpContextAccessor = httpContextAccessor;
            _persistentHelper = persistentHelper;
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

        public void Update(ConfigurationFormModel formModel, HttpRequest httpRequest, Func<IModelBinderFacade, ModelBinderContext> modelBindingContextBuilder)
        {
            if (_postbackHelper.IsPostback(httpRequest))
            {
                var bindingContext = modelBindingContextBuilder(_modelBinder);
                _modelBinder.TryUpdateModel(formModel, bindingContext);
            }
            else
            {
                var providerName = _persistentHelper.LoadProviderName();
                if (providerName != null)
                    formModel.VirtualFileProvider.CurrentProviderName = providerName;
            }
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

            _persistentHelper.SaveProviderName(providers.CurrentProviderName);
            _persistentHelper.SaveActiveGrp(activeGrp.ProviderKey);
        }
    }
}