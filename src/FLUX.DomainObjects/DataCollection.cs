using System.Collections.Generic;

namespace FLUX.DomainObjects
{
    public class DataCollection
    {
        public string OriginalValue { get; set; }
        public string PostbackName { get; set; }

        public IEnumerable<LayerValueViewModel> LayerValues { get; set; }

        public bool IsLastElement { get; set; }
    }
}
