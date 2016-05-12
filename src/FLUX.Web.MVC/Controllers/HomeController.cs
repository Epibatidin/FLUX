using System.Collections.Generic;
using DataAccess.Interfaces;
using Extraction.Interfaces;
using Extraction.Interfaces.Layer;
using Facade.MVC;
using FLUX.Interfaces;
using Microsoft.AspNet.Mvc;

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
        
        public IActionResult DataDelivery()
        {
            return ViewComponent("DataDelivery");
        }
    }
}
