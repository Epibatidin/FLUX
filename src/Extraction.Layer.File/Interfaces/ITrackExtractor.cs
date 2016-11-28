using System.Collections.Generic;

namespace Extraction.Layer.File.Interfaces
{
    public interface ITrackExtractor
    {
        bool Execute(IList<FileLayerSongDo> fileLayerSongs, int actualCD);
    }
}