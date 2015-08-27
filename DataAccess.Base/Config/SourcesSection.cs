using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using DataAccess.FileSystem.Config;
using DataAccess.XMLStub.Config;
using Extension.Configuration;

namespace DataAccess.Base.Config
{
    public class SourcesSection : ConfigurationSection, IEnumerator<ConfigurationProperty>
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
        public ConfigurationProperty Current
        {
            get
            {
                return _enumerator.Current as ConfigurationProperty;
               
            }
        }

        public IEnumerable<IKeyedElement> GetPropertyAsKeyedElements(ConfigurationProperty property)
        {
            if (property == null)
                throw new ArgumentNullException("ConfigurationProperty is null in Iterator");

            return base[property] as IEnumerable<IKeyedElement>;
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
            if(_enumerator != null)
                _enumerator.Reset();
            _enumerator = null;
        }

        public void Dispose()
        {
            _enumerator = null;
        }
    }
}
