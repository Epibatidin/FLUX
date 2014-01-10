using System.Web.Mvc;

namespace FLUXMVC.Controllers
{
    public class MasterController : ProcessorCoupledController 
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}
