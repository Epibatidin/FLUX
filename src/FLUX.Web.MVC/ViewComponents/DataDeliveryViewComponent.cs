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
        private readonly IExtractionContextBuilder _extractionContextBuilder;
        private readonly IExtractionProcessor _extractionProcessor;
        private readonly ILayerResultJoiner _resultJoiner;

        public DataDeliveryViewComponent(IExtractionContextBuilder extractionContextBuilder,
            ILayerResultJoiner resultJoiner, IExtractionProcessor extractionProcessor)
        {
            _extractionContextBuilder = extractionContextBuilder;        
            _extractionProcessor = extractionProcessor;
            _resultJoiner = resultJoiner;
        }
        
        public IViewComponentResult Invoke()
        {
            var extractionContext = _extractionContextBuilder.Build();
            if(extractionContext == null)
                return new EmptyViewComponentResult();
            
            _extractionProcessor.Execute(extractionContext);
            
            var layerData = _resultJoiner.Build(extractionContext.SourceValues,
                extractionContext.Iterate().Select(c => c.Data).ToArray());
            
            return View("Index", layerData);
        }
    }
}
