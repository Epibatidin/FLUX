using System.Collections.Generic;
using MP3Renamer.Models.DataContainer.EntityInterfaces;
using MP3Renamer.Models.Extraction.Extracter;


namespace MP3Renamer.Models.Extraction
{
    public class LeafDataExtractor : FilterManager, ILeafExtractionHelper
    {
        ISubRoot SubRoot;

        public void WorkUnit(ISubRoot workunit)
        {
            SubRoot = workunit;
        }

        public ISubRoot execute()
        {
            if (SubRoot == null) return null;


            //Track();
            foreach (var item in getExtractor())
            {
                item.WorkUnit(SubRoot);
                item.execute();
            }
            Name();

            return SubRoot;
        }

        private IEnumerable<ILeafExtractionHelper> getExtractor()
        {
            yield return new TrackLeafExtractor();
        }



        

        private void Name()
        {

            for (int current = 0; current < SubRoot.Leafs.Count; current++)
            {
                SubRoot.Leafs[current].Name = SubRoot.Leafs[current].StringManager.Join(' ');
            }
        }
    }
}