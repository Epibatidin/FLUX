using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using AbstractDataExtraction;
using ConfigurationExtensions.Interfaces;
using ExtractionLayerProcessor.Config;
using ExtractionLayerProcessor.Processor;

namespace ExtractionLayerProcessor
{
    // http://msdn.microsoft.com/en-us/library/c02as0cs%28loband%29.aspx
    public class ExtractionProcessorFactory
    {
        // hier quasi die typen auslesen und einfach nach einander basteln 
        // dazu gehört nactürlich auch das configurieren 
        // ausserdem muss ich alle files einlesen und in die layer injecten 
       
        public ExtractionProcessor Create(ExtractionLayerConfig layerconfig)
        {
            DataStore dataStore = new DataStore();
            List<IDataExtractionLayer> configuredLayers = CreateConfiguredLayers(layerconfig, dataStore);

            ExtractionProcessor processor = null;

            if (layerconfig.ASync)
                ;
            else 
                processor = new SequentielExtractionProcessor();

            processor.Init(configuredLayers, dataStore);

            return null;
        }

        private List<IDataExtractionLayer> CreateConfiguredLayers(ExtractionLayerConfig layerconfig, DataStore dataStore)
        {
            var result = new List<IDataExtractionLayer>(); 
            foreach (var config in layerconfig.LayerCollection)
            {
                if (!config.IsActive) continue;
                var layerType = Type.GetType(config.ExtractorType);
                var curLayer = Activator.CreateInstance(layerType) as IDataExtractionLayer;

                curLayer.Configure(config.SectionData);
                var layerStore = dataStore.Register(config.Key);

                curLayer.SetUpdater(layerStore);

                result.Add(curLayer);
            }
            return result;
        }
    }
}
