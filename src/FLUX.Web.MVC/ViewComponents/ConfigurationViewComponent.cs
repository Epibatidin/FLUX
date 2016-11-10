using FLUX.Interfaces.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace FLUX.Web.MVC.ViewComponents
{
    public class ConfigurationViewComponent : ViewComponent
    {
        private readonly IConfigurationFormProcessor _configurationFormProcessor;
        private readonly IPostbackHelper _postbackHelper;
        private readonly IActionContextAccessor _actionBindingContextAccessor;

        public ConfigurationViewComponent(IConfigurationFormProcessor configurationFormProcessor, 
            IPostbackHelper postbackHelper, IActionContextAccessor actionBindingContextAccessor)
        {
            _configurationFormProcessor = configurationFormProcessor;
            _postbackHelper = postbackHelper;
            _actionBindingContextAccessor = actionBindingContextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            
            var fm = _configurationFormProcessor.Build();
            _configurationFormProcessor.Update(fm, Request, c => c.BuildContext(this,_actionBindingContextAccessor));

            if (_postbackHelper.IsPostback(Request))
                _configurationFormProcessor.Process(fm);

            return View("Index", fm);
        }
    }
}
