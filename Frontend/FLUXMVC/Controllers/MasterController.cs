using System.Web.Mvc;

namespace FLUXMVC.Controllers
{
    public class MasterController : ProcessorCoupledControllerBase 
    {
        public ViewResult Index()
        {
            var processor = GetProcessor();
            return View();
        }
    }
}
