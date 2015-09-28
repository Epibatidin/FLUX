using System.Web.Mvc;
using DataAccess.Interfaces;
using FLUX.Interfaces.Web;

namespace FLUX.Web.MVC.Controllers
{
    public class ConfigurationController : Controller
    {
        private readonly IConfigurationFormModelBuilder _selectionBuilder;
        private readonly IVirtualFileConfigurationReader _configurationReader;

        public ConfigurationController(IConfigurationFormModelBuilder selectionBuilder, 
            IVirtualFileConfigurationReader configurationReader)
        {
            _selectionBuilder = selectionBuilder;
            _configurationReader = configurationReader;
        }

        public PartialViewResult Index()
        {
            var availableProviders = _configurationReader.ReadToDO();
            var fm = _selectionBuilder.BuildFormModel();
            

            return null;
        }

    }
}



////public class FileProviderController : Controller
////{
////    //public PartialViewResult Index()
////    //{
////    //    return PartialView("Index", SessionHandler);
////    //}

////    public PartialViewResult Submit(FormCollection form)
////    {
////        TryUpdateModel(SessionHandler, form.ToValueProvider());
////        if (ModelState.IsValid)
////        {
////            GetProcessor().Refresh(SessionHandler.GetVirtualFileProvider());
////        }
////        return PartialView("ProviderSelection", SessionHandler.Providers);
////    }
////}
