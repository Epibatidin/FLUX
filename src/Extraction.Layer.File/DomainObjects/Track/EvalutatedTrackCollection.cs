using System.Collections.Generic;

namespace Extraction.Layer.File.DomainObjects.Track
{
    public class EvalutatedTrackCollection
    {
        public IList<ProperplySureTrack> Tracks { get; private set; }

        public bool? UseRelativeBegin { get; set; }

        public EvalutatedTrackCollection()
        {
            Tracks = new List<ProperplySureTrack>();
        }
    }

}
