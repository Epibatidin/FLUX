using System.Collections.Generic;

namespace Extraction.Layer.File.DomainObjects.Track
{
    public class AllTrackContainer
    {
        public int FoundIn { get; private set; }

        public List<PossibleTrack> PossibleTracks;

        public AllTrackContainer(int foundIn)
        {
            FoundIn = foundIn;
            PossibleTracks = new List<PossibleTrack>();
        }
    }
}
