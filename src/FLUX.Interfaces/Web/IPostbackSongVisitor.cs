using DataAccess.Interfaces;
using FLUX.DomainObjects;

namespace FLUX.Interfaces.Web
{
    public interface IPostbackTreeVisitor
    {
        void Add(ArtistNode artistNode);
        void Add(AlbumNode albumNode);
        void Add(CdNode cdNode);
        void Add(SongNode songNode);

        IExtractionValueFacade Current { get; }
    }
}
