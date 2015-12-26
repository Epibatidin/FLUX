using FLUX.Interfaces.Web;
using Microsoft.AspNet.Mvc;

namespace FLUX.Web.MVC.Controllers
{
    public class ConfigurationController : Controller
    {
        private readonly IConfigurationFormModelBuilder _configurationFormModelBuilder;
        private readonly IPostbackHelper _postbackHelper;

        public ConfigurationController(IConfigurationFormModelBuilder configurationFormModelBuilder, IPostbackHelper postbackHelper)
        {
            _configurationFormModelBuilder = configurationFormModelBuilder;
            _postbackHelper = postbackHelper;
        }

        public PartialViewResult Index()
        {
            var fm = _configurationFormModelBuilder.Build();
            _configurationFormModelBuilder.Update(fm, this);

            if (_postbackHelper.IsPostback(Request))
                _configurationFormModelBuilder.Process(fm);

            return PartialView("Index", fm);
        }
    }
}