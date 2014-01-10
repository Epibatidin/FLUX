using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MP3LibIntegrater
{
    class Executor
    {
        private Artist artist;

        private List<Dictionary<string, string>> mh = new List<Dictionary<string, string>>(); 



        public Executor(Artist artist)
        {
            // TODO: Complete member initialization
            this.artist = artist;
        }

        internal void GatherAvailableInformation()
        {
            // okay ich muss in meinem artisten der ja bereits alle sub inittalisiert hat 
            artist.ProduceCleanInformation();            
        }

        internal void MakePersistent()
        {
            //FormatGatheredInformation();
            mh2();


        }


        private IEnumerable<KeyValuePair<string,string>> FormatGatheredInformation()
        {
            // da kommt jedes Attribu mit seinem zusammnengesetzen werten rein 

            // die relvanten informationen in album sind der album name und das jahr
            Dictionary<string, string> sdf = null;
            foreach(Album album in artist.Entities)
            {                
                foreach(Song song in album.Entities)
                {
                    sdf = new Dictionary<string, string>();
                    //sdf.Add("artist", artist.getChoppedInfo[0]);
                    sdf.Add("year", album.Year);
                    //sdf.Add("albumname", joinString(album.relevantInformation));
                    sdf.Add("cdCount", album.CDcount.ToString());
                    sdf.Add("tracknum", song.TrackNum.ToString());
                    //sdf.Add("trackname", joinString(song.relevantInformation));
                }
                mh.Add(sdf);
            }
            return null;
        }

        private string ApplyFormat(Artist artist, Album album, Song song)
        {
            StringBuilder s = new StringBuilder();
            Dictionary<string, string> sdf = new Dictionary<string, string>();
            //sdf.Add("artist", artist.getChoppedInfo[0]);
            sdf.Add("year", album.Year);
            //sdf.Add("albumname", joinString(album.relevantInformation));
            sdf.Add("cdCount", album.CDcount.ToString());
            sdf.Add("tracknum", song.TrackNum.ToString());
            //sdf.Add("trackname", joinString(song.relevantInformation));


            return s.ToString();
        }


        private void mh2()
        {            
            // nimm dir den artisten und schau ob es diesen ordner schon gibt 
            DirectoryInfo artistDir = Algs.AddDirectory(null, artist.ArtistName);

            foreach (Album album in artist.Entities)
            {
                string albumFolderName = album.Year + " - " +
                            album.AlbumName;
                DirectoryInfo albumDir = Algs.AddDirectory(artistDir, albumFolderName);

                foreach (Song song in album.Entities)
                {
                    DirectoryInfo songDir = albumDir;
                    if (album.CDcount > 1)
                    {
                        songDir = Algs.AddDirectory(albumDir, "CD" + song.OffCD);
                    }
                    string songName =
                        song.createWriteAbleTracknum() + " - " + song.SongName + " - " +
                        artist.ArtistName;
                    Algs.CopyFile(songDir, songName, song.AbsoluteSongFilePath);

                }
                
            }




        }
    }
}
