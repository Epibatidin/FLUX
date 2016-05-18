using System.Collections.Generic;
using System.Linq;
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
        private readonly ILayerResultJoiner _resultJoiner;

        public DataDeliveryViewComponent(IVirtualFilePeristentHelper persistentHelper, 
            IVirtualFileConfigurationReader configurationReader, IExtractionProcessor extractionProcessor,
            ILayerResultJoiner resultJoiner)
        {
            _persistentHelper = persistentHelper;
            _configurationReader = configurationReader;
            _extractionProcessor = extractionProcessor;
            _resultJoiner = resultJoiner;
        }

        private IDictionary<int, IVirtualFile> LoadSources()
        {
            var providerName = _persistentHelper.LoadProviderName();
            var activeGrp = _persistentHelper.LoadActiveGrp();

            if (providerName == null || activeGrp == null) return null;

            var virtualFileFactory = _configurationReader.FindActiveFactory(activeGrp);

            var sources = _persistentHelper.LoadSource(virtualFileFactory.GetVirtualFileArrayType());
            if (sources != null) return sources;

            var context = _configurationReader.BuildContext(providerName);
            sources = virtualFileFactory.RetrieveVirtualFiles(context);
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
            
            var layerData = _resultJoiner.Build(source, extractionContext.Iterate().Select(c => c.Data).ToArray());
            
            return View("MainDataTable", layerData);
        }
    }
}
