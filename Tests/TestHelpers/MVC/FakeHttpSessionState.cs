using System.Collections.Generic;
using System.Web;

namespace TestHelpers.MVC
{
    public class FakeHttpSessionState : HttpSessionStateBase
    {
        private Dictionary<string, object> _valDictionary; 

        public FakeHttpSessionState()
        {
            _valDictionary = new Dictionary<string, object>();
        }
        
        public override object this[string name]
        {
            get
            {
                if (_valDictionary.ContainsKey(name))
                    return _valDictionary[name];
                return null;
            }
            set
            {
                if (_valDictionary.ContainsKey(name))
                    _valDictionary[name] = value;
                else
                    _valDictionary.Add(name, value);
            }
        }
    }
}
