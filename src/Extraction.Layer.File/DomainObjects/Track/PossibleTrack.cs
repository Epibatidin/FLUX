namespace Extraction.Layer.File.DomainObjects.Track
{
    public class PossibleTrack
    {
        private int _relativeBegin;
        private int _relativeEnd;

        public int FoundValue { get; private set; }

        public int RelativeBegin() => _relativeBegin;
        public int RelativeEnd() => _relativeEnd;

        public PossibleTrack(int foundAt, int foundValue, int stringLength)
        {
            _relativeBegin = foundAt;
            _relativeEnd = stringLength - foundAt;

            FoundValue = foundValue;
        }
    }
}
