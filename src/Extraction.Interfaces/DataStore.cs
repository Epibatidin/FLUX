using System.Collections.Generic;
using Extension.IEnumerable;

namespace Extraction.Interfaces
{
    public class DataStore
    {
        private readonly Dictionary<string, UpdateObject> _dataCollection;

        public IList<int> Keys { get; set; }

        public DataStore()
        {
            _dataCollection = new Dictionary<string, UpdateObject>();
        }

        private UpdateObject CreateNew()
        {
            return new UpdateObject();
        }

        public UpdateObject Register(string key)
        { 
            return _dataCollection.GetOrCreate(key,CreateNew);
        }

        public IEnumerable<UpdateObject> Iterate()
        {
            return _dataCollection.Values;
        }
    }
}
