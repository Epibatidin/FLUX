using Facade.Session;
using Microsoft.AspNet.Mvc;

namespace FLUX.Web.MVC.ViewComponents
{
    public class WhateverViewComponent : ViewComponent
    {
        private readonly ISessionFacade _sessionFacade;

        public WhateverViewComponent(ISessionFacade sessionFacade)
        {
            _sessionFacade = sessionFacade;
        }

        public IViewComponentResult Invoke()
        {
            return View("Index", HttpContext.Items["Files"]);
        }

    }
}
