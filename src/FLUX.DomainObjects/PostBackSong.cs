using Extraction.Interfaces;

namespace FLUX.DomainObjects
{
    public class PostbackSong : ISong
    {
        public int Id { get; set; }

        public string Artist { get; set; }

        public int Year { get; set; }
        public string Album { get; set; }
        public string CD { get; set; }

        public int TrackNr { get; set; }
        public string SongName { get; set; }
    }
}
