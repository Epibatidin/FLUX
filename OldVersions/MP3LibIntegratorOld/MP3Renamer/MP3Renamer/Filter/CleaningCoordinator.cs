using System.Collections.Generic;
using MP3Renamer.Filter.Interfaces;
using MP3Renamer.Filter.Cleaner.RootWorkunit;
using MP3Renamer.Filter.Cleaner.SubrootWorkunit;

namespace MP3Renamer.Models.Extraction
{
    public class CleaningCoordinator : AbstractCoordinator
    {
        protected override IEnumerable<IRootWorkunit> RootWorkunit()
        {
            yield return new RootEqualityCleaner();
            yield return new RootOneStepUpCleaner();
        }

        protected override IEnumerable<ISubrootWorkunit> SubrootWorkunit()
        {
            yield return new SubrootEqualityCleaner();
            yield return new SubrootOneStepUpCleaner();
        }

        protected override IEnumerable<ILeafWorkunit> LeafWorkunit()
        {
            yield break;
        }
    }
}