using DataStructure.Tree;

namespace Extraction.Interfaces
{
    public interface ISong : IUnique<int>
    {
        int TrackNr { get; }
        int Year { get; }
        
        string Artist { get; }
        string Album { get; }
        string CD { get; }
        string SongName { get; }
    }
}
