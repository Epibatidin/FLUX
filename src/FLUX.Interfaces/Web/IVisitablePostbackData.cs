namespace FLUX.Interfaces.Web
{
    public interface IVisitablePostbackData
    {
        void Accept(IPostbackSongBuilder visitor);
    }
}
