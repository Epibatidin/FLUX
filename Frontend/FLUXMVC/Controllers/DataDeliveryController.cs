using System.Web.Mvc;
using FLUXMVC.ViewModels;

namespace FLUXMVC.Controllers
{
    public class DataDeliveryController : ProcessorCoupledControllerBase
    {
        private DataStoreMVCAdapter GetAdapter()
        {
             return new DataStoreMVCAdapter(GetProcessor().DataStore);
        } 


        public PartialViewResult Index()
        {
            return PartialView("MainDataTable", GetAdapter().LayerData);
        }
        
    }
}
