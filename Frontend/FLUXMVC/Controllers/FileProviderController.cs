using System.Web.Mvc;

namespace FLUXMVC.Controllers
{
    public class FileProviderController : ProcessorCoupledControllerBase
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
