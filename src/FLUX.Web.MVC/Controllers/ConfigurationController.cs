using FLUX.Interfaces.Web;
using Microsoft.AspNet.Mvc;

namespace FLUX.Web.MVC.Controllers
{
    public class ConfigurationController : Controller
    {
        private readonly IConfigurationFormModelBuilder _configurationFormModelBuilder;

        public ConfigurationController(IConfigurationFormModelBuilder configurationFormModelBuilder)
        {
            _configurationFormModelBuilder = configurationFormModelBuilder;
        }

        public PartialViewResult Index()
        {
            var fm = _configurationFormModelBuilder.Build();
            _configurationFormModelBuilder.Update(fm, this);

            return PartialView("Index", fm);
        }
    }
}