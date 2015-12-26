using FLUX.Interfaces.Web;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Infrastructure;

namespace FLUX.Web.MVC.ViewComponents
{
    public class ConfigurationViewComponent : ViewComponent
    {
        private readonly IConfigurationFormModelBuilder _configurationFormModelBuilder;
        private readonly IPostbackHelper _postbackHelper;
        private readonly IActionBindingContextAccessor _actionBindingContextAccessor;

        public ConfigurationViewComponent(IConfigurationFormModelBuilder configurationFormModelBuilder, IPostbackHelper postbackHelper, IActionBindingContextAccessor actionBindingContextAccessor)
        {
            _configurationFormModelBuilder = configurationFormModelBuilder;
            _postbackHelper = postbackHelper;
            _actionBindingContextAccessor = actionBindingContextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            var fm = _configurationFormModelBuilder.Build();
            _configurationFormModelBuilder.Update(fm, Request, c => c.BuildContext(this,_actionBindingContextAccessor));

            if (_postbackHelper.IsPostback(Request))
                _configurationFormModelBuilder.Process(fm);

            return View("Index", fm);
        }
    }
}
