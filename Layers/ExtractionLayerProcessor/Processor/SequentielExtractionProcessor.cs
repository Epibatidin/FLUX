using System.Collections.Generic;
using AbstractDataExtraction;
using Interfaces.VirtualFile;

namespace ExtractionLayerProcessor.Processor
{
    public class SequentielExtractionProcessor : ExtractionProcessor
    {
        private IList<IDataExtractionLayer> _layers;

        protected override void AddLayers(IList<IDataExtractionLayer> layer)
        {
            _layers = layer;
        }

        protected override void InternalSetData(int constPathLength, Dictionary<int, IVirtualFile> _data)
        {
            foreach (var item in _layers)
            {
                item.InitData(constPathLength, _data);
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
