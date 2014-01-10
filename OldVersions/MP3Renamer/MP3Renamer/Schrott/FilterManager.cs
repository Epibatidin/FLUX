//using System.Collections.Generic;
//using System.Linq;
//using MP3Renamer.Models.Extraction;

//namespace MP3Renamer.Models
//{
//    public enum WorkType : byte 
//    {
//        None,
//        Root, 
//        Subroot, 
//        Leaf
//    }

//    public enum TransformType : byte
//    {
//        None , 
//        Cleaner,
//        Filter ,
//        Extraction
//    }


//    public class FilterManager
//    {
//        private class HelperContainer
//        {
//            public TransformType TransformType {get;private set;}
//            public WorkType WorkType {get;private set;}
            
//            public IExtractionHelper Helper {get; private set;}

//            public HelperContainer(TransformType TransType, WorkType WorkType, IExtractionHelper Helper)
//            {
//                this.TransformType = TransType;
//                this.WorkType = WorkType;
//                this.Helper = Helper;
//            }
            
//        }

//        private List<HelperContainer> Helpers;


//        public FilterManager()
//        {
//            Helpers = new List<HelperContainer>();
            
//            HelperContainer con = new HelperContainer(TransformType.Extraction, WorkType.Subroot, new TrackExtractor());
//            ((ISubrootWorkunit)con.Helper).Workunit = null;

//            Helpers.Add(con);
//            Helpers[0].Helper.Execute();
//        }
        

//        public IEnumerable<IExtractionHelper> GetByTransformType(TransformType transType)
//        {
//            return Helpers.Where(c => c.TransformType == transType).Select(c => c.Helper);
//        }

//    }
//}