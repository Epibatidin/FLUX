using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Facade.Session
{
    public interface ISessionFacade
    {
        void SetObject(HttpContext context, string key, object value);
        void SetObject(ISession session, string key, object value);
        TValue GetObject<TValue>(HttpContext context, string key) where TValue : class;
        TValue GetObject<TValue>(ISession session, string key) where TValue : class;
    }

    public class SessionFacade : ISessionFacade
    {
        public void SetObject(HttpContext context, string key, object value)
        {
            SetObject(context.Session, key, value);
        }

        public void SetObject(ISession session, string key, object value)
        {
            var serializeObject = JsonConvert.SerializeObject(value);
            session.SetString(key, serializeObject);
        }

        public TValue GetObject<TValue>(HttpContext context, string key) where TValue : class
        {
            return GetObject<TValue>(context.Session,key);
        }

        public TValue GetObject<TValue>(ISession session, string key) where TValue : class
        {
            var strvalue = session.GetString(key);
            return JsonConvert.DeserializeObject<TValue>(strvalue);
        }
    }
}
