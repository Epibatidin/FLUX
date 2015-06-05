using System.Web;

namespace FLUX.Interfaces.Web
{
    public interface IHttpContextProvider
    {
        HttpContextBase Current();

        string MapPath(string filepath);
    }
}
