using System;
using System.Web;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle.Scoped;

namespace FLUX.Configuration.Windsor.Lifestyle
{
    // http://webcache.googleusercontent.com/search?q=cache:http://stackoverflow.com/questions/1366214/castle-project-per-session-lifestyle-with-asp-net-mvc&gws_rd=cr&ei=AYVYVfqAHYqu7gaC14LYDw
    public class PerCookieLifestyleAdapter : IScopeAccessor
    {
        private static ICache<string, ILifetimeScope> _knownReferences = new PerCookieLifestyleCacheFacade();

        public PerCookieLifestyleAdapter()
        {
            
        }

        public PerCookieLifestyleAdapter(ICache<string, ILifetimeScope> knownReferences)
        {
            _knownReferences = knownReferences;
        }


        public void Dispose()
        {
            if(HttpContext.Current == null) return;

            Dispose(new HttpContextWrapper(HttpContext.Current));
        }

        public void Dispose(HttpContextBase httpContext)
        {
            var cookie = httpContext.Request.Cookies["LifeStyle"];
            var currentScope = _knownReferences.GetItem(cookie.Value);
            
            if(currentScope != null)
                currentScope.Dispose();
        }

        public ILifetimeScope GetScope(CreationContext context)
        {
            if (HttpContext.Current == null) 
                throw new InvalidOperationException("HttpContext.Current is null. PerWebSessionLifestyle can only be used in ASP.Net");
            
            return GetScope(new HttpContextWrapper(HttpContext.Current));
        }

        public ILifetimeScope GetScope(HttpContextBase httpContext)
        {
            var cookie = httpContext.Request.Cookies["LifeStyle"];
            ILifetimeScope currentScope;
            if (cookie == null)
            {
                currentScope = new DefaultLifetimeScope(new ScopeCache());
                string key = Guid.NewGuid().ToString();
                _knownReferences.SetItem(key, currentScope);
                httpContext.Request.Cookies.Add(new HttpCookie("LifeStyle", key));
            }
            else
                currentScope = _knownReferences.GetItem(cookie.Value);

            return currentScope;
        }
    }
}