using System;
using System.Collections.Generic;
using System.Configuration;
using AbstractDataExtraction;
using ExtractionLayerProcessor.Config;

namespace ExtractionLayerProcessor
{
    public class ExtractionLayerFactory
    {
        // hier quasi die typen auslesen und einfach nach einander basteln 
        // dazu gehört nactürlich auch das configurieren 
        // ausserdem muss ich alle files einlesen und in die layer injecten 
        private IEnumerable<IExtractionLayerConfigItem> _layerconfigs;

        public ExtractionLayerFactory(IEnumerable<IExtractionLayerConfigItem> layerconfig)
        {
            _layerconfigs = layerconfig;
        }


        public IEnumerable<IDataExtractionLayer> GetConfiguredLayers(DataStore store)
        {
            foreach (var item in _layerconfigs)
            {
                var layer = Activator.CreateInstance(item.LayerType) as IDataExtractionLayer;
                
                ConfigurationSection section = null;
                if(!String.IsNullOrWhiteSpace(item.ConfigSectionName))
                    section = (ConfigurationSection)ConfigurationManager.GetSection(item.ConfigSectionName);

                layer.Configure(section);

                layer.SetUpdater(store.Register(item.Key));

                yield return layer;
            }
            yield break;
        }


        public IEnumerable<string> Get_SourceData()
        {



            return null;
        }


    }
}
