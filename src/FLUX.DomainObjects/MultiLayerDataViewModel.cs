using Extraction.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FLUX.DomainObjects
{
    public class MultiLayerDataViewModel
    {
        private class SortWrapper
        {
            public int Count;
            public LayerValueViewModel Content;
        }

        private int forLevel;

        public IList<string> Keys { get; set; }
        private MultiLayerDataContainer Container { get; set; }

        public string AdditionalClass { get; set; }

        public string NamePattern { get; set; }

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
            dataCollection.PostbackName = NamePattern + "." +key;

            List<string> list = null;
            Container.Data.TryGetValue(key, out list);              
            dataCollection.LayerValues = BuildContainer(list);

            return dataCollection;
        }



        public IEnumerable<LayerValueViewModel> BuildContainer(IList<string> values)
        {
            var dict = new Dictionary<string, SortWrapper>(StringComparer.CurrentCultureIgnoreCase);
            int valuesCount = 0;
            if (values != null)
            {
                valuesCount = values.Count;
                for (int i = 0; i < values.Count; i++)
                {
                    var value = values[i];
                    if (string.IsNullOrEmpty(value)) continue;

                    SortWrapper container;
                    if (!dict.TryGetValue(value, out container))
                    {
                        container = new SortWrapper();
                        container.Content = new LayerValueViewModel(values.Count, value);

                        dict.Add(value, container);
                    }

                    container.Count++;
                    container.Content.ColorCodeActiveFlags[i] = true;
                }
            }
            if (dict.Count == 0)
                return new[] { new LayerValueViewModel(valuesCount, null) };

            return dict.Values.OrderByDescending(c => c.Count).Select(c => c.Content);
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
