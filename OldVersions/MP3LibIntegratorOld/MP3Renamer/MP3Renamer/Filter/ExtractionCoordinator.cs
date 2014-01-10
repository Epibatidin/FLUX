using System.Collections.Generic;
using MP3Renamer.Filter.Extraction.LeafWorkunit;
using MP3Renamer.Filter.Extraction.SubrootWorkunit;
using MP3Renamer.Filter.Interfaces;

namespace MP3Renamer.Models.Extraction
{
    public class ExtractionCoordinator : AbstractCoordinator
    {
        // der kann sich doch bitte um alle extraktions bedürfnisse kümmern
        // dann kann das aus dem FileListViewModel raus      

        protected override IEnumerable<IRootWorkunit> RootWorkunit()
        {
            yield break;
        }

        protected override IEnumerable<ISubrootWorkunit> SubrootWorkunit()
        {
            yield return new YearExtractor();
            yield return new TrackExtractor();
            yield return new SubrootNameExtractor();
        }

        protected override IEnumerable<ILeafWorkunit> LeafWorkunit()
        {
            yield return new LeafNameExtractor();
        }
    }
}