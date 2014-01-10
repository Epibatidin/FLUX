using System.Collections.Generic;
using System.Linq;

using MP3Renamer.Models.DataContainer.EntityInterfaces;
using MP3Renamer.Models.Extraction.Cleaner;
using MP3Renamer.Models.DataContainer;


namespace MP3Renamer.Models.Extraction
{
    public class LeafDataFilter : FilterManager,  ILeafExtractionHelper
    {
        public ISubRoot SubRoot { get; private set; }
              
        //-----------------------------------------------------------------------------------------------------------------------
        public LeafDataFilter() { }
        //-----------------------------------------------------------------------------------------------------------------------
        
        //-----------------------------------------------------------------------------------------------------------------------
        public void WorkUnit(ISubRoot workunit)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            this.SubRoot = workunit;
        }

        //-----------------------------------------------------------------------------------------------------------------------
        public ISubRoot execute()
        //-----------------------------------------------------------------------------------------------------------------------
        {
            if (SubRoot == null) return null;

            List<IStringPartManager> StringParts = SubRoot.Leafs.Select(c => c.StringManager).ToList();

            foreach (IMultiFilter mf in MultiFilter())
            {
                StringParts = mf.Filter(StringParts);
            }

            for (int currentLeaf = 0; currentLeaf < StringParts.Count; currentLeaf++)
            {
                foreach (IFilter sf in SingleFilter())
                {
                    StringParts[currentLeaf] = sf.Filter(StringParts[currentLeaf]);
                }
                SubRoot.Leafs[currentLeaf].StringManager = StringParts[currentLeaf];
            }
            return SubRoot;
        }


        //-----------------------------------------------------------------------------------------------------------------------
        private IEnumerable<IMultiFilter> MultiFilter()
        //-----------------------------------------------------------------------------------------------------------------------
        {
            if (FilterIsSet((byte)LeafCleanLevel.Equalities))
                yield return new EqualityCleaner();

            yield break;
        }


        //-----------------------------------------------------------------------------------------------------------------------
        private IEnumerable<IFilter> SingleFilter()
        //-----------------------------------------------------------------------------------------------------------------------
        {
            if (FilterIsSet((byte)LeafCleanLevel.Internet))
                yield return new InternetStuffCleaner();

            if (FilterIsSet((byte)LeafCleanLevel.OneStepUp))
            {
                var f = new OneStepUpCleaner();
                f.AddOneStepUp(DataManager.Get.Current.Name);
                f.AddOneStepUp(SubRoot.Name);
                yield return f;
            }
            //if (FilterIsSet(LeafCleanLevel.ShortWords))
            //    yield return new ShortWordCleaner();


            yield break;
        }        
    }
}