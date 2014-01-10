using System;
using System.Collections.Generic;
using System.Configuration;
using AbstractDataExtraction;
using Extensions;
using ExtractionLayerProcessor.Config;
using Interfaces.Config;
using Interfaces.VirtualFile;

namespace ExtractionLayerProcessor.Processor
{
    public abstract class ExtractionProcessor
    {
        private static readonly Dictionary<Guid, ExtractionProcessor> Instances = new Dictionary<Guid, ExtractionProcessor>();

        public static ExtractionProcessor Get(Guid Key)
        {
            return Instances.GetOrCreate(Key, Create);
        }

        private static ExtractionProcessor Create()
        {
            var section = (ExtractionLayerConfig)ConfigurationManager.GetSection("ExtractionLayer");
            
            ExtractionProcessor processor;
            if (section.ASync)
                processor = null;
            else
                processor = new SequentielExtractionProcessor();
            
            //processor.Setup(new ExtractionLayerFactory(section.Layers));
            return processor;
        }
        
        public DataStore DataStore { get; private set; }

        public Progress Progress { get; protected set; }


        private void Setup(ExtractionLayerFactory fac)
        {
            DataStore = new DataStore();
            int layerCount = 0;
            foreach (var item in fac.GetConfiguredLayers(DataStore))
            {
                AddLayer(item);
                layerCount++;
            }
            Progress = new Progress(layerCount);            
        }

        protected abstract void AddLayer(IDataExtractionLayer layer);

        public void Refresh(IVirtualFileProvider reader)
        {
            //if (!reader.MoveNext()) throw new ArgumentOutOfRangeException("Cant Move Next No Data to handle");
            //if (reader.constPathLength == 0) throw new NotSupportedException("Please specify a root Path Length");

            //var _data = reader.Current;

            //if (_data == null)
            //    throw new ArgumentNullException("No LayerData to Handle");

            //if (!_data.Any())
            //    throw new ArgumentOutOfRangeException("LayerData contains no Elements");
            //DataStore.Register("Init").UpdateData(_data);
            //InternalSetData(reader.constPathLength, _data);
        }

        protected abstract void InternalSetData(int constPathLength, Dictionary<int, IVirtualFile> _data); 

        public abstract void Execute();
    }
}
