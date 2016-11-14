using System.Collections.Generic;

namespace Extraction.Layer.File.FullTreeOperators.InnerOperators
{
    public interface ITrackExtractor
    {
        bool Execute(IList<FileLayerSongDo> partedStrings);
    }
}