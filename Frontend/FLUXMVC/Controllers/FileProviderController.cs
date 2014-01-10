using System.Web.Mvc;
using System.Web.Routing;
using FLUXMVC.Components;
using Interfaces.Frontend;

namespace FLUXMVC.Controllers
{
    public class FileProviderController : ProcessorCoupledController
    {
        public PartialViewResult Index()
        {
            return PartialView("Index", SessionHandler);
        }

        public PartialViewResult Submit(FormCollection form)
        {
            TryUpdateModel(SessionHandler, form.ToValueProvider());
            if (ModelState.IsValid)
            {
                GetProcessor().Refresh(SessionHandler.GetVirtualFileProvider());
            }
            return PartialView("ProviderSelection" , SessionHandler.Providers);
        }
    }
}
