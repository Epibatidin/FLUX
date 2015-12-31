using DataStructure.Tree;

namespace Extraction.Interfaces
{
    public interface ISong : IUnique<int>
    {
        int CD { get; }
        int TrackNr { get; }
        int Year { get; }

        string Artist { get; }
        string Album { get; }
        string SongName { get; }
    }
}
