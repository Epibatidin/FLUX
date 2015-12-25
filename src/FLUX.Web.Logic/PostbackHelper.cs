using FLUX.Interfaces.Web;

namespace FLUX.Web.Logic
{
    public class PostbackHelper : IPostbackHelper 
    {
        public bool IsPostback()
        {
            return false;
        }
    }
}
