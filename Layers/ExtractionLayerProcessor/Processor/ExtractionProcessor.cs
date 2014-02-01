using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using AbstractDataExtraction;
using ConfigurationExtensions.Interfaces;
using Extensions;
using ExtractionLayerProcessor.Config;
using Interfaces.Config;
using Interfaces.VirtualFile;

namespace ExtractionLayerProcessor.Processor
{
    public abstract class ExtractionProcessor
    {
        private static readonly Dictionary<Guid, ExtractionProcessor> Instances = new Dictionary<Guid, ExtractionProcessor>();

        public DataStore DataStore { get; private set; }

        public Progress Progress { get; protected set; }

        public static ExtractionProcessor Get(Guid key, IConfigurationLocator configLocator)
        {
            ExtractionProcessor processor;
            if (Instances.ContainsKey(key))
                processor = Instances[key];
            else
            {
                processor = Create(configLocator);
                Instances.Add(key, processor);
            }
            return processor;
        }
        
        private static ExtractionProcessor Create(IConfigurationLocator configLocator)
        {
            var config = configLocator.Locate().GetSection("ExtractionLayer") as ExtractionLayerConfig;
            var factory = new ExtractionProcessorFactory();
            return factory.Create(config);
        }

        public void Init(IList<IDataExtractionLayer> configuredLayers, DataStore dataStore)
        {
            AddLayers(configuredLayers);

            Progress = new Progress(configuredLayers.Count);

            DataStore = dataStore;
        }

        public abstract void Execute();

        protected abstract void AddLayers(IList<IDataExtractionLayer> layers);


        public void Refresh(IVirtualFileProvider fileProvider)
        {
            var _data = fileProvider[fileProvider.RootNames[0]];

            if(_data == null)
                throw new ArgumentNullException("No LayerData to Handle");
            if (_data.Count == 0)
                throw new ArgumentOutOfRangeException("LayerData contains no Elements");

            DataStore.Keys = _data.Keys.ToArray();
            InternalSetData(_data);
        }

        protected abstract void InternalSetData(Dictionary<int, IVirtualFile> data); 
        
        

        
    }
}
