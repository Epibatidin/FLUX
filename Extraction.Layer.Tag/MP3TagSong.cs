using Extraction.Interfaces;

namespace Extraction.Layer.Tag
{
    public class MP3TagSong : ISong
    {
        public int ID { get; set; }
        public int CD {get;set;}
        public int TrackNr {get;set;}
        public int Year { get; set; }

        public string Artist { get; set; }
        public string Album { get; set; }
        public string SongName {get;set;}
    }
}
