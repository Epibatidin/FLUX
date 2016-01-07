using System.Collections.Generic;
using DataAccess.Interfaces;
using Extension.IEnumerable;

namespace Extraction.Interfaces
{
    public class ExtractionContext
    {
        private readonly Dictionary<string, UpdateObject> _dataCollection;

        public IDictionary<int, IVirtualFile> SourceValues { get; set; }
        public IVirtualFileConfigurationReader StreamReader { get; set; }

        public IList<int> Keys { get; set; }

        public ExtractionContext()
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
