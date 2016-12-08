using System.ComponentModel.DataAnnotations;
using Extraction.Interfaces;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;

namespace FLUX.DomainObjects
{
    public class PostbackSong : ISong, IExtractionValueFacade
    {
        private PostbackSong current;

        public PostbackSong()
        {

        }

        public PostbackSong(PostbackSong current)
        {
            Id = current.Id;

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

        public IEnumerable<Tuple<string, string>> ToValues()
        {
            yield return Tuple.Create("Artist", Artist);

            yield return Tuple.Create("Year", Year.ToString());
            yield return Tuple.Create("Album", Album);

            yield return Tuple.Create("CD", CD);

            yield return Tuple.Create("TrackNr", TrackNr.ToString());
            yield return Tuple.Create("SongName", SongName);
        }
    }
}
