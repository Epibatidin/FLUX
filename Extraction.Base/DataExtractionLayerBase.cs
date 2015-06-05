using System.Collections.Generic;
using System.Xml;
using AbstractDataExtraction;
using DataAccess.Interfaces;
using Extraction.Interfaces;

namespace Extraction.Base
{
    public abstract class DataExtractionLayerBase : IDataExtractionLayer
    {
        public void Configure(XmlNode config)
        {
            ConfigureInternal(new XmlNodeReader(config));
        }

        protected abstract void ConfigureInternal(XmlReader reader);

        public abstract void InitData(Dictionary<int, IVirtualFile> dirtyData);
        
        public Progress Progress { get; private set; }

        protected void CreateProgress(int total)
        {
            Progress = new Progress(total);
        }
           
        private UpdateObject _uo;
        public void SetUpdater(UpdateObject uo)
        {
            _uo = uo;
        }

        protected abstract ISongByKeyAccessor Data();

        protected virtual void Update()
        {
            Progress.DoProgress();
            _uo.UpdateData(Data());
        }

        public abstract void Execute();
    }
}
