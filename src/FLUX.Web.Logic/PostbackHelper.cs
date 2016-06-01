using System;
using FLUX.Interfaces.Web;
using Microsoft.AspNetCore.Http;

namespace FLUX.Web.Logic
{
    public class PostbackHelper : IPostbackHelper 
    {
        public bool IsPostback(HttpContext context)
        {
            return IsPostback(context.Request);
        }

        public bool IsPostback(HttpRequest request)
        {
            return string.Compare(request.Method, "post", StringComparison.CurrentCultureIgnoreCase) == 0;
        }
    }
}
