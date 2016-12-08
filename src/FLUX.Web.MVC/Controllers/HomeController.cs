using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;


namespace FLUX.Web.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Configuration()
        {
            return ViewComponent("Configuration");
        }


        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ResetSession()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }


        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult DataDelivery()
        {
            return ViewComponent("DataDelivery");
        }
    }
}
