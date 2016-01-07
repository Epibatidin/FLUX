//using System;
//using System.Collections.Generic;
//using Extraction.Base.Config;
//using Extraction.Base.Processor;
//using Extraction.Interfaces;
//using Extraction.Interfaces.Layer;

//namespace Extraction.Base
//{
//    // http://msdn.microsoft.com/en-us/library/c02as0cs%28loband%29.aspx
//    public class ExtractionProcessorFactory : IExtractionProcessorFactory
//    {
//        private readonly IExtractionLayerConfigurationProvider _configuration;
//        private readonly IDataStoreProvider _dataStoreProvider;

//        public ExtractionProcessorFactory(IExtractionLayerConfigurationProvider configuration, IDataStoreProvider dataStoreProvider)
//        {
//            _configuration = configuration;
//            _dataStoreProvider = dataStoreProvider;
//        }

//        // hier quasi die typen auslesen und einfach nach einander basteln 
//        // dazu gehört nactürlich auch das configurieren 
//        // ausserdem muss ich alle files einlesen und in die layer injecten 
       
//        public IExtractionProcessor Create()
//        {
//            ExtractionContext dataStore = _dataStoreProvider.Current();
//            List<IDataExtractionLayer> configuredLayers = CreateConfiguredLayers(_configuration.ExtractionLayerConfig, dataStore);

//            ExtractionProcessor processor = new SequentielExtractionProcessor();

//            //if (layerconfig.ASync)
//            //    ;
//            //else 
//            //    processor = new SequentielExtractionProcessor();

//            processor.Init(configuredLayers, dataStore);

//            return processor;
//        }

//        private List<IDataExtractionLayer> CreateConfiguredLayers(ExtractionLayerConfig layerconfig, ExtractionContext dataStore)
//        {
//            var result = new List<IDataExtractionLayer>(); 
//            foreach (var config in layerconfig.LayerCollection)
//            {
//                if (!config.IsActive) continue;
//                var layerType = Type.GetType(config.ExtractorType);
//                var curLayer = Activator.CreateInstance(layerType) as IDataExtractionLayer;

//                curLayer.Configure(config.SectionData);
//                var layerStore = dataStore.Register(config.Key);

//                curLayer.SetUpdater(layerStore);

//                result.Add(curLayer);
//            }
//            return result;
//        }
//    }
//}
