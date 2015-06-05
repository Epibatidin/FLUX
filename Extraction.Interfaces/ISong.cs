
namespace Extraction.Interfaces
{
    public interface ISong : IUnique<int>
    {
        int ID { get; }

        int CD { get; }
        int TrackNr { get; }
        int Year { get; }

        string Artist { get; }
        string Album { get; }
        string SongName { get; }
    }
}
