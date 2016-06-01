using DataAccess.Interfaces;
using Extraction.Interfaces;
using Extraction.Interfaces.Layer;
using Facade.MVC;
using FLUX.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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

        private bool TryFillExtractionContext(ExtractionContext extractionContext)
        {
            var providerName = _persistentHelper.LoadProviderName();
            var activeGrp = _persistentHelper.LoadActiveGrp();

            if (providerName == null || activeGrp == null) return false;

            var context = _configurationReader.BuildContext(providerName);

            var virtualFileFactory = _configurationReader.FindActiveFactory(activeGrp);
            extractionContext.StreamReader = virtualFileFactory.GetReader(context);

            extractionContext.SourceValues = _persistentHelper.LoadSource(virtualFileFactory.GetVirtualFileArrayType());

            if (extractionContext.SourceValues == null)
            {
                extractionContext.SourceValues = virtualFileFactory.RetrieveVirtualFiles(context);
                _persistentHelper.SaveSource(extractionContext.SourceValues);
            }
            return true;
        } 
        
        public IViewComponentResult Invoke()
        {
            var extractionContext = new ExtractionContext();
            if(!TryFillExtractionContext(extractionContext))
                return new EmptyViewComponentResult();
            
            _extractionProcessor.Execute(extractionContext);
            
            var layerData = _resultJoiner.Build(extractionContext.SourceValues, extractionContext.Iterate().Select(c => c.Data).ToArray());
            
            return View("Index", layerData);
        }
    }
}
