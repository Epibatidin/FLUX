using Microsoft.AspNetCore.Http;

namespace FLUX.Interfaces.Web
{
    public interface IPostbackHelper
    {
        bool IsPostback(HttpContext context);
        bool IsPostback(HttpRequest context);
    }
}
