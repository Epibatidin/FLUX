using System;
using DataAccess.Interfaces;
using FLUX.DomainObjects;
using FLUX.Interfaces.Web;
using Microsoft.AspNet.Mvc;
using Facade.MVC;
using Facade.Session;
using Microsoft.AspNet.Http;

namespace FLUX.Web.Logic
{
    public class ConfigurationFormProcessor : IConfigurationFormProcessor
    {
        private readonly IVirtualFileConfigurationReader _configProvider;
        private readonly IPostbackHelper _postbackHelper;
        private readonly IModelBinderFacade _modelBinder;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISessionFacade _sessionFacade;

        public ConfigurationFormProcessor(IVirtualFileConfigurationReader configProvider, 
            IPostbackHelper postbackHelper, IModelBinderFacade modelBinder, IHttpContextAccessor httpContextAccessor, ISessionFacade sessionFacade)
        {
            _configProvider = configProvider;
            _postbackHelper = postbackHelper;
            _modelBinder = modelBinder;
            _httpContextAccessor = httpContextAccessor;
            _sessionFacade = sessionFacade;
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

        public void Update(ConfigurationFormModel formModel, HttpRequest httpRequest, Func<IModelBinderFacade, ModelBinderContext> controller)
        {
            if (_postbackHelper.IsPostback(httpRequest))
            {
                var bindingContext = controller(_modelBinder);
                _modelBinder.TryUpdateModel(formModel, bindingContext);
            }
        }

        public void Update(ConfigurationFormModel formModel, Controller controller)
        {
            
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

            var context = _httpContextAccessor.HttpContext;
            context.Items["Files"] = _configProvider.GetVirtualFiles(providers.CurrentProviderName, activeGrp.ProviderKey);
        }
    }
}