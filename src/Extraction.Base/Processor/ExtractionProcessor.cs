//using System.Collections.Generic;
//using AbstractDataExtraction;
//using DataAccess.Interfaces;
//using Extraction.Interfaces;
//using Extraction.Interfaces.Layer;

//namespace Extraction.Base.Processor
//{
//    public abstract class ExtractionProcessor : IExtractionProcessor
//    {
//        public ExtractionContext ExtractionContext { get; private set; }

//        public Progress Progress { get; protected set; }

//        public void Init(IList<IDataExtractionLayer> configuredLayers, ExtractionContext dataStore)
//        {
//            AddLayers(configuredLayers);

//            Progress = new Progress(configuredLayers.Count);

//            ExtractionContext = dataStore;
//        }

//        public abstract void Execute();

//        protected abstract void AddLayers(IList<IDataExtractionLayer> layers);
        
//        //public void Refresh(IVirtualFileProvider fileProvider)
//        //{
//        //    var _data = fileProvider[fileProvider.RootNames[0]];

//        //    if(_data == null)
//        //        throw new ArgumentNullException("No LayerData to Handle");
//        //    if (_data.Count == 0)
//        //        throw new ArgumentOutOfRangeException("LayerData contains no Elements");

//        //    ExtractionContext.Keys = _data.Keys.ToArray();
//        //    InternalSetData(_data);
//        //}

//        protected abstract void InternalSetData(Dictionary<int, IVirtualFile> data); 
//    }
//}
