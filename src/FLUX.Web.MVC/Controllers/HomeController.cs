using Microsoft.AspNet.Mvc;

namespace FLUX.Web.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //HttpContext

            return View();
        }

        public IActionResult Configuration()
        {
            return ViewComponent("Configuration");
        }

        public IActionResult DataDelivery()
        {
            return ViewComponent("DataDelivery");
        }
    }
}
