using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ExtractionLayerProcessor.Processor;
using FLUXMVC.Components;
using Interfaces.Frontend;

namespace FLUXMVC.Controllers
{
    public class ProcessorCoupledControllerBase : Controller
    {
        protected IVirtualFileProviderSessionHandler SessionHandler;
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            SessionHandler = new VirtualFileProviderSessionHandler(requestContext.HttpContext.Session,
                new WebConfigurationLocator(requestContext.HttpContext, "VirtualFileProvider"));
        }

        protected ExtractionProcessor GetProcessor()
        {
            Guid Key;
            var cookie = Request.Cookies.Get("ProcessorKey");
            if (cookie == null)
            {
                cookie = new HttpCookie("ProcessorKey");
                Key = Guid.NewGuid();
                cookie.Value = Key.ToString();
                Response.AppendCookie(cookie);
            }
            else
            {
                Key = new Guid(cookie.Value);
            }
            return ExtractionProcessor.Get(Key, new WebConfigurationLocator(HttpContext, "Layer"));
        }

    }
}
