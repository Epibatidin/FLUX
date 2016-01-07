using System.Collections.Generic;
using DataAccess.Interfaces;
using Extraction.Interfaces;
using FLUX.Web.Logic;
using Microsoft.AspNet.Mvc;

namespace FLUX.Web.MVC.ViewComponents
{
    public class DataDeliveryViewComponent : ViewComponent
    {
        public DataDeliveryViewComponent()
        {
            
        }


        public IViewComponentResult Invoke()
        {
            var resultjoiner = new LayerResultJoiner(HttpContext.Items["Files"] as IDictionary<int, IVirtualFile>);

            return View("MainDataTable", resultjoiner.LayerData);
        }
    }
}
