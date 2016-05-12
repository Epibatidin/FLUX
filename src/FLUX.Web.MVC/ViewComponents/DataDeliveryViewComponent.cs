using System.Collections.Generic;
using DataAccess.Interfaces;
using Extraction.Interfaces;
using Extraction.Interfaces.Layer;
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
        private readonly IExtractionProcessor _extractionProcessor;

        public DataDeliveryViewComponent(IVirtualFilePeristentHelper persistentHelper, 
            IVirtualFileConfigurationReader configurationReader, IExtractionProcessor extractionProcessor)
        {
            _persistentHelper = persistentHelper;
            _configurationReader = configurationReader;
            _extractionProcessor = extractionProcessor;
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

            var extractionContext = new ExtractionContext()
            {
                SourceValues = source
            };
            _extractionProcessor.Execute(extractionContext);

            var resultjoiner = new LayerResultJoiner(source);

            foreach (var uo in extractionContext.Iterate())
            {
                resultjoiner.Add(uo.Data);
            }
            return View("MainDataTable", resultjoiner.LayerData);
        }
    }
}
