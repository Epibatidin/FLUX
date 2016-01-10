using System.Collections.Generic;
using DataAccess.Interfaces;
using Extraction.Interfaces;
using Facade.MVC;
using FLUX.Interfaces;
using FLUX.Web.Logic;
using Microsoft.AspNet.Mvc;

namespace FLUX.Web.MVC.ViewComponents
{
    public class DataDeliveryViewComponent : ViewComponent
    {
        private readonly IVirtualFilePeristentHelper _persistentHelper;
        private readonly IVirtualFileConfigurationReader _configurationReader;

        public DataDeliveryViewComponent(IVirtualFilePeristentHelper persistentHelper, IVirtualFileConfigurationReader configurationReader)
        {
            _persistentHelper = persistentHelper;
            _configurationReader = configurationReader;
        }

        private IDictionary<int, IVirtualFile> LoadSources()
        {
            var providerName = _persistentHelper.LoadProviderName();
            var activeGrp = _persistentHelper.LoadActiveGrp();

            if (providerName == null || activeGrp == null) return null;
            
            var reader = _configurationReader.RetrieveReader(activeGrp);

            var sources = _persistentHelper.LoadSource(reader.GetVirtualFileArrayType());
            if (sources != null) return sources;

            sources = _configurationReader.GetVirtualFiles(providerName, activeGrp);
            _persistentHelper.SaveSource(sources);

            return sources;
        } 


        public IViewComponentResult Invoke()
        {
            var source = LoadSources();
            if (source == null) return new EmptyViewComponentResult();

            var resultjoiner = new LayerResultJoiner(source);

            return View("MainDataTable", resultjoiner.LayerData);
        }
    }
}
