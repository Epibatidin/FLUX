using FLUX.Interfaces.Web;
using DataAccess.Interfaces;
using FLUX.DomainObjects;

namespace FLUX.Web.Logic
{
    public class PostbackTreeVisitor : IPostbackTreeVisitor
    {
        private PostbackSong _current;

        public IExtractionValueFacade Current
        {
            get
            {
                var newSong = new PostbackSong(_current);
                var dummy = _current;
                _current = newSong;
                return dummy;
            }
        }
        
        public PostbackTreeVisitor()
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
            _current.Album = albumNode.Album;
            _current.Year = albumNode.Year;
        }

        public void Add(ArtistNode artistNode)
        {
            _current.Artist = artistNode.Artist;
        }        
    }
}
