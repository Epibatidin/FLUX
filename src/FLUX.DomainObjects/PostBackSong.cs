using System.ComponentModel.DataAnnotations;
using Extraction.Interfaces;

namespace FLUX.DomainObjects
{
    public class PostbackSong : ISong
    {
        private PostbackSong current;

        public PostbackSong()
        {

        }

        public PostbackSong(PostbackSong current)
        {
            Artist = current.Artist;

            Year = current.Year;
            Album = current.Album;

            CD = current.CD;

            TrackNr = current.TrackNr;
            SongName = current.SongName;
        }

        public int Id { get; set; }

        [Required]
        public string Artist { get; set; }

        public int Year { get; set; }

        [Required]
        public string Album { get; set; }
        public string CD { get; set; }

        public int TrackNr { get; set; }
        [Required]
        public string SongName { get; set; }
    }
}
