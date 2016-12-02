using Extraction.Interfaces;
using System;
using System.Collections.Generic;

namespace FLUX.DomainObjects
{
    public class MultiLayerDataViewModel
    {
        private int forLevel;

        public IList<string> Keys { get; set; }
        private MultiLayerDataContainer Container { get; set; }

        public string AdditionalClass { get; set; }

        public MultiLayerDataViewModel(MultiLayerDataContainer container, string additionalClass,
            int forLvl, string[] keys)
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

        public DataCollection RetrieveData(string key, bool isLast)
        {
            var dataCollection = new DataCollection();
            dataCollection.IsLastElement = isLast;
            dataCollection.OriginalValue = OriginalValue;

            List<string> list = null;

            if (!Container.Data.TryGetValue(key, out list))
                list = new List<string>();
            dataCollection.LayerValues = list;
            return dataCollection;
        }

        public int ID
        {
            get
            {
                return Container.Id;
            }
        }

        public IEnumerable<Tuple<string, string>> IteratePostbackData()
        {
            yield return Tuple.Create(nameof(ISong.Id), Container.Id.ToString());

            foreach (var item in Container.Data)
            {
                if (item.Value.Count == 0) continue;
                if (string.IsNullOrEmpty(item.Value[0])) continue;

                yield return Tuple.Create(item.Key, item.Value[0]);
            }
            yield break;
        }

    }
}
