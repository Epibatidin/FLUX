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

        private Tuple<string, string>[] _asValues;
        public IList<Tuple<string, string>> ToValues()
        {
            if (_asValues != null)
                return _asValues;

            _asValues = new Tuple<string, string>[6];
            _asValues[0] = Tuple.Create("Artist", Artist);
            _asValues[1] = Tuple.Create("Album", Album);
            _asValues[2] = Tuple.Create("CD", CD);
            _asValues[3] = Tuple.Create("SongName", SongName);

            _asValues[4] = Tuple.Create("Year", Year.ToString());
            _asValues[5] = Tuple.Create("TrackNr", TrackNr.ToString());
            return _asValues;
        }
    }
}
