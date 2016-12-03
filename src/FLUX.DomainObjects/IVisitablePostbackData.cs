using System;

namespace FLUX.DomainObjects
{
    public interface IPostbackSongBuilder
    {
        void Add(ArtistNode artistNode);
        void Add(AlbumNode albumNode);
        void Add(CdNode cdNode);
        void Add(SongNode songNode);
    }

    public class PostbackSongBuilder : IPostbackSongBuilder
    {
        private PostbackSong _current;

        public PostbackSong Current
        {
            get
            {
                var newSong = new PostbackSong(_current);
                var dummy = _current;
                _current = newSong;
                return dummy;
            }
        } 

        public PostbackSongBuilder()
        {
            _current = new PostbackSong();                
        }


        public void Add(CdNode cdNode)
        {
            _current.CD = cdNode.CD;                
        }

        public void Add(SongNode songNode)
        {
            _current.SongName = songNode.SongName;
            _current.TrackNr = songNode.TrackNr;
            _current.Id = songNode.Id;
        }

        public void Add(AlbumNode albumNode)
        {
            Current.Album = albumNode.Album;
            Current.Year = albumNode.Year;                
        }

        public void Add(ArtistNode artistNode)
        {
            Current.Artist = artistNode.Artist;
        }
    }


    public interface IVisitablePostbackData
    {
        void Accept(IPostbackSongBuilder visitor);
    }
}