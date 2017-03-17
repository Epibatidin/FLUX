using Extraction.Interfaces;

namespace Extraction.Layer.Tags.DomainObjects
{
    public class TagSong : ISong
    {
        public string Album { get; set; }
        public string Artist { get; set; }
        public string CD { get; set; }
        public int Id { get; set; }
        public string SongName { get; set; }
        public int TrackNr { get; set; }
        public int Year { get; set; }        
    }
}
