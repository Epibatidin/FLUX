using System.Collections.Generic;
using System.Linq;

using MP3Renamer.Models.DataContainer;
using MP3Renamer.Models.DataContainer.EntityInterfaces;
using MP3Renamer.Models.Extraction.Cleaner;


namespace MP3Renamer.Models.Extraction
{
    public class SubRootDataFilter : FilterManager, ISubRootExtractionHelper
    {
        public IRoot Root { get; private set; }

        //-----------------------------------------------------------------------------------------------------------------------
        public SubRootDataFilter() { }
        //-----------------------------------------------------------------------------------------------------------------------
                


        //-----------------------------------------------------------------------------------------------------------------------
        public void WorkUnit(IRoot workunit)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            this.Root = workunit;
        }
                
        public IRoot execute()
        {
            if (Root == null) return null;

            List<IStringPartManager> StringParts = Root.SubRoots.Select(c => c.StringManager).ToList();
        
            foreach (IMultiFilter mf in MultiFilter())
            {
                StringParts = mf.Filter(StringParts);
            }            
            
            for (int currentSub = 0; currentSub < StringParts.Count; currentSub++)
            {
                foreach (IFilter sf in SingleFilter())
                {
                    StringParts[currentSub] = sf.Filter(StringParts[currentSub]);
                }
                Root.SubRoots[currentSub].StringManager = StringParts[currentSub];
            }
            return Root;            
        }

        //-----------------------------------------------------------------------------------------------------------------------        
        private IEnumerable<IMultiFilter> MultiFilter()
        //-----------------------------------------------------------------------------------------------------------------------
        {
            if (FilterIsSet((byte)SubRootCleanLevel.Equalities))
                yield return new EqualityCleaner();

            yield break;        
        }


        //-----------------------------------------------------------------------------------------------------------------------
        private IEnumerable<IFilter> SingleFilter()
        //-----------------------------------------------------------------------------------------------------------------------
        {
            if (FilterIsSet((byte)SubRootCleanLevel.Internet))
                yield return new InternetStuffCleaner();

            if (FilterIsSet((byte)LeafCleanLevel.OneStepUp))
            {
                var f = new OneStepUpCleaner();
                f.AddOneStepUp(DataManager.Get.Current.Name);
                f.AddOneStepUp(Root.Name);
                yield return f;
            }

            if (FilterIsSet((byte)SubRootCleanLevel.ShortWords))
                //yield return new ShortWordCleaner();

            yield break;
        }


  
    }
}