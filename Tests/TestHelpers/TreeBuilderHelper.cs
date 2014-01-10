using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestHelpers
{
    public static class TreeBuilderHelper
    {
        public static ItemBuilder<Song, int> Song()
        {
            List<Func<int, int, int, Song>> facs = new List<Func<int, int, int, Song>>();
            facs.Add((rid, i, width) =>
            {
                return new Song()
                {
                    Artist = "Artist",
                    ID = 10000 * width
                };
            });

            facs.Add((rid, level, width) =>
            {
                return new Song()
                {
                    Album = "Album" + width,
                    ID = rid + width * 1000
                };
            });

            facs.Add((rid, level, width) =>
            {
                return new Song()
                {
                    Artist = "CD ",
                    ID = rid + width * 100
                };
            });

            facs.Add((rid, level, width) =>
            {
                return new Song()
                {
                    SongName = "Song",
                    TrackNr = width,
                    ID = rid + width
                };
            });

            return new ItemBuilder<Song, int>(0,facs);
        }       



    }
}
