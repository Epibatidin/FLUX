using Extraction.Interfaces;
using System;

namespace Extraction.Base
{
    public class NonOverwrittingSongDummy : ISong
    {
        public int Id { get; set; }
        public int CD { get; set;  }
        public int TrackNr { get; set; }
        public int Year { get; set; }
        public string Artist { get;set;  }
        public string Album { get; set; }
        public string SongName { get; set; }

        public void Add(ISong song)
        {
            CD = Set(song, c => c.CD);
            TrackNr = Set(song, c => c.TrackNr);
            Year = Set(song, c => c.Year);

            Artist = Set(song, c => c.Artist);
            Album = Set(song, c => c.Album);
            SongName = Set(song, c => c.SongName);
        }

        private string Set(ISong song, Func<ISong, string> getter)
        {
            var value = getter(song);
            if (string.IsNullOrEmpty(value))
                return getter(this);

            return value;
        }

        private int Set(ISong song, Func<ISong, int> getter)
        {
            var value = getter(song);
            if (value == 0)
                return getter(this);

            return value;
        }
    }
}
