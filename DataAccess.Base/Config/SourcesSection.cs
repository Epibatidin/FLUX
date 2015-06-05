using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using DataAccess.FileSystem.Config;
using DataAccess.XMLStub.Config;

namespace DataAccess.Base.Config
{
    public class SourcesSection : ConfigurationSection, IEnumerator<string>
    {
        [ConfigurationProperty("XML", IsRequired = true)]
        public XMLSourcesCollection XML
        {
            get
            {
                return (XMLSourcesCollection)base["XML"];
            }
        }

        [ConfigurationProperty("Directory", IsRequired = true)]
        public DirectorySourcesCollection Directory
        {
            get
            {
                return (DirectorySourcesCollection)base["Directory"];
            }
        }
        
        private IEnumerator _enumerator;
        public string Current
        {
            get
            {
                var prop = _enumerator.Current as ConfigurationProperty;
                return prop.Name;
            }
        }

        public bool MoveNext()
        {
            if (_enumerator == null) _enumerator = Properties.GetEnumerator();

            return _enumerator.MoveNext();
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public void Reset()
        {
            _enumerator = null;
        }

        public void Dispose()
        {
            _enumerator = null;
        }
    }
}
