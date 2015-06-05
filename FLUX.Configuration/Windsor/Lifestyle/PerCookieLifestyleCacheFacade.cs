using System;
using System.Web.Caching;
using Castle.MicroKernel.Lifestyle.Scoped;

namespace FLUX.Configuration.Windsor.Lifestyle
{
    public class PerCookieLifestyleCacheFacade : ICache<string , ILifetimeScope>
    {
        private readonly Cache _internalcache;
        
        public PerCookieLifestyleCacheFacade()
        {
            _internalcache = new Cache();
        }

        public ILifetimeScope GetItem(string key)
        {
            return _internalcache.Get(key) as ILifetimeScope;
        }

        public void SetItem(string key, ILifetimeScope value)
        {
            _internalcache.Add(key, value, null, DateTime.Now.AddHours(1), new TimeSpan(0, 15, 00),
                CacheItemPriority.High, null);
        }
    }
}