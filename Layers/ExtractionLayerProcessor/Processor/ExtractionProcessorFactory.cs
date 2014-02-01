using System;
using System.Collections.Generic;
using AbstractDataExtraction;
using ExtractionLayerProcessor.Config;
using Interfaces;

namespace ExtractionLayerProcessor.Processor
{
    // http://msdn.microsoft.com/en-us/library/c02as0cs%28loband%29.aspx
    public class ExtractionProcessorFactory
    {
        // hier quasi die typen auslesen und einfach nach einander basteln 
        // dazu gehört nactürlich auch das configurieren 
        // ausserdem muss ich alle files einlesen und in die layer injecten 
       
        public ExtractionProcessor Create(ExtractionLayerConfig layerconfig)
        {
            DataStore<ISong> dataStore = new DataStore<ISong>();
            List<IDataExtractionLayer<ISong>> configuredLayers = CreateConfiguredLayers(layerconfig, dataStore);

            ExtractionProcessor processor = new SequentielExtractionProcessor();

            //if (layerconfig.ASync)
            //    ;
            //else 
            //    processor = new SequentielExtractionProcessor();

            processor.Init(configuredLayers, dataStore);

            return processor;
        }

        private List<IDataExtractionLayer<T>> CreateConfiguredLayers<T>(ExtractionLayerConfig layerconfig, DataStore<T> dataStore) where T : class 
        {
            var result = new List<IDataExtractionLayer<T>>(); 
            foreach (var config in layerconfig.LayerCollection)
            {
                if (!config.IsActive) continue;
                var layerType = Type.GetType(config.ExtractorType);
                var curLayer = Activator.CreateInstance(layerType) as IDataExtractionLayer<T>;

                curLayer.Configure(config.SectionData);
                var layerStore = dataStore.Register(config.Key);

                curLayer.SetUpdater(layerStore);

                result.Add(curLayer);
            }
            return result;
        }
    }
}
