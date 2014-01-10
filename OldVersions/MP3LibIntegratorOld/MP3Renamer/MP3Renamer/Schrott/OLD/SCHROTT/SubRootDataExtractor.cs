//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.IO;

//using MP3Renamer.Models.DataContainer;
//using MP3Renamer.Models.DataContainer.EntityInterfaces;
//using MP3Renamer.Models.Helper;


//namespace MP3Renamer.Models.Extraction
//{ 
//    public class SubRootDataExtractor : FilterManager, ISubRootExtractionHelper
//    {
//        public IRoot Root{get ; private set;}

//        //-----------------------------------------------------------------------------------------------------------------------
//        public SubRootDataExtractor() { }
//        //-----------------------------------------------------------------------------------------------------------------------
        
        
//        //-----------------------------------------------------------------------------------------------------------------------
//        public void WorkUnit(IRoot workunit)
//        //-----------------------------------------------------------------------------------------------------------------------
//        {
//            this.Root = workunit;
//        }


//        //-----------------------------------------------------------------------------------------------------------------------
//        public IRoot execute()
//        //-----------------------------------------------------------------------------------------------------------------------
//        {
//            if (Root == null) return null;

//            Year();

//            Name();
                                   
//            return Root;
//        }




//        //-----------------------------------------------------------------------------------------------------------------------
//        private void Year()
//        //-----------------------------------------------------------------------------------------------------------------------
//        {
//            if (FilterIsSet((byte)SubRootExtractLevel.Year))
//            {
//                Root.SubRoots = ExtractYear(Root.SubRoots);            
//            }
//        }

    

//        //-----------------------------------------------------------------------------------------------------------------------
//        private void Name()
//        //-----------------------------------------------------------------------------------------------------------------------
//        {
//            if (FilterIsSet((byte)SubRootExtractLevel.Name))
//            {
//                Root.SubRoots = ExtractName(Root.SubRoots);
//            }
//        }


//        //-----------------------------------------------------------------------------------------------------------------------
//        private List<ISubRoot> ExtractName(List<ISubRoot> SubRoots)
//        //-----------------------------------------------------------------------------------------------------------------------
//        {
//            for (int current = 0; current < SubRoots.Count; current++)
//            {
//                var parts = SubRoots[current].StringManager.RawDataParts;

//                if (parts != null && parts.Count != 0)
//                {
//                    SubRoots[current].Name = SubRoots[current].StringManager.Join(' ');
//                }
//                else
//                {
//                    SubRoots[current].Name = Root.Name;
//                }
//            }
//            return SubRoots;
//        }    
//    }
//} 