using System.Collections.Generic;
using DataAccess.Interfaces;
using Extraction.Interfaces;

namespace Extraction.Base.Processor
{
    public class SequentielExtractionProcessor : ExtractionProcessor
    {
        private IList<IDataExtractionLayer> _layers;

        protected override void AddLayers(IList<IDataExtractionLayer> layer)
        {
            _layers = layer;
        }

        protected override void InternalSetData(Dictionary<int, IVirtualFile> data)
        {
            foreach (var item in _layers)
            {
                item.InitData(data);
            }
        }

        public override void Execute()
        {
            foreach (var item in _layers)
            {
                item.Execute();                
            }
        }
    }
}
