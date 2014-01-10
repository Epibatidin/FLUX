using System.Collections.Generic;
using System.Configuration;

using Interfaces.VirtualFile;

namespace AbstractDataExtraction
{
    public abstract class DataExtractionLayerBase : IDataExtractionLayer
    {
        public virtual void Configure(ConfigurationSection config)
        {
        }

        public abstract void InitData(int constPathLength, Dictionary<int, IVirtualFile> _dirtyData);
        
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

        protected abstract object Data();

        protected virtual void Update()
        {
            Progress.DoProgress();
            _uo.UpdateData(Data());
        }

        public abstract void Execute();

    }
}
