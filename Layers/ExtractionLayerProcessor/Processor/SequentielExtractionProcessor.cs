using System.Collections.Generic;
using AbstractDataExtraction;
using Interfaces.VirtualFile;

namespace ExtractionLayerProcessor.Processor
{
    public class SequentielExtractionProcessor : ExtractionProcessor
    {
        private List<IDataExtractionLayer> layers;

        public SequentielExtractionProcessor()
        {
            layers = new List<IDataExtractionLayer>();
        }

        protected override void AddLayer(IDataExtractionLayer layer)
        {
            layers.Add(layer);
        }

        protected override void InternalSetData(int constPathLength, Dictionary<int, IVirtualFile> _data)
        {
            foreach (var item in layers)
            {
                item.InitData(constPathLength, _data);
            }
        }


        public override void Execute()
        {
            foreach (var item in layers)
            {
                item.Execute();                
            }

        }
    }
}
