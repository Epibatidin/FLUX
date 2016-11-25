using System.Collections.Generic;

namespace FLUX.DomainObjects
{
    public class MultiLayerDataViewModel
    {
        private int forLevel;
        
        public IList<string> Keys { get; set; }
        private MultiLayerDataContainer Container { get; set; }
        
        public string AdditionalClass { get; set; }

        public MultiLayerDataViewModel(MultiLayerDataContainer container,string additionalClass, int forLvl, string[] keys)
        {
            forLevel = forLvl;
            Container = container;
            Keys = keys;
            AdditionalClass = additionalClass;
        }

        public string OriginalValue
        {
            get
            {
                return Container.GetOriginalValue(forLevel);
            }
        }

        public IList<string> RetrieveData(string key)
        {
            List<string> list = null;

            if (!Container.Data.TryGetValue(key, out list))
                list = new List<string>();

            return list;
        }

    }
}
