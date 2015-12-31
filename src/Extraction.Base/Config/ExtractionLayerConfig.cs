using System.Collections.Generic;

namespace Extraction.Base.Config
{
    public class ExtractionLayerConfig 
    {
        public bool ASync { get; set; }

        public ExtractionLayerConfigItem[] Layers { get; set; }
    }
}
