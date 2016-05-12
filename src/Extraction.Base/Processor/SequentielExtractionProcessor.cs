using System.Collections.Generic;
using DataAccess.Interfaces;
using Extraction.Interfaces;
using Extraction.Interfaces.Layer;

namespace Extraction.Base.Processor
{
    public class SequentielExtractionProcessor : IExtractionProcessor
    {
        private readonly IEnumerable<IDataExtractionLayer> _extractionLayers;

        public SequentielExtractionProcessor(IEnumerable<IDataExtractionLayer> extractionLayers)
        {
            _extractionLayers = extractionLayers;
        }
        
        public void Execute(ExtractionContext extractionContext)
        {
            foreach (var layer in _extractionLayers)
            {
                var uo = extractionContext.Register(layer.GetType().Name);

                layer.Execute(extractionContext, uo);
            }
        }
    }
}
