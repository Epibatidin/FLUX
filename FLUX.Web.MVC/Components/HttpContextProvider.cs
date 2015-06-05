using System.Web;
using FLUX.Interfaces.Web;

namespace FLUX.Web.MVC.Components
{
    public class HttpContextProvider : IHttpContextProvider
    {
        public HttpContextBase Current()
        {
            return new HttpContextWrapper(HttpContext.Current);
        }

        public string MapPath(string filepath)
        {
            return HttpContext.Current.Server.MapPath(filepath);
        }
    }
}