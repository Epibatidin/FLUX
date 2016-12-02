using System.Collections.Generic;

namespace FLUX.DomainObjects
{
    public class DataCollection
    {
        public string OriginalValue { get; set; }

        public IList<string> LayerValues { get; set; }

        public bool IsLastElement { get; set; }
    }
}
