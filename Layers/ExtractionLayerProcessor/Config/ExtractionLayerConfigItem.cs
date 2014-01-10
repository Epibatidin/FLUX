using System;
using System.Configuration;
using ConfigurationExtensions.Interfaces;

namespace ExtractionLayerProcessor.Config
{
    public class ExtractionLayerConfigItem : ConfigurationElement,IKeyedElement, IExtractionLayerConfigItem
    {
        [ConfigurationProperty("Name", IsRequired = true, IsKey = true)]
        public string Key
        {
            get
            {
                return (string)base["Name"];
            }
        }

        private Type _layertype;
        public Type LayerType
        {
            get
            {
                if(_layertype == null)
                {
                    _layertype = Type.GetType(layer, true, true);
                }
                return _layertype;
            }
        }

        [ConfigurationProperty("LayerType", IsRequired = true)]
        private string layer
        {
            get
            {
                return (string)base["LayerType"];
            }
        }

        [ConfigurationProperty("ConfigSectionName", IsRequired = false)]
        public string ConfigSectionName
        {
            get
            {
                return (string)base["ConfigSectionName"];
            }
        }
    }
}
