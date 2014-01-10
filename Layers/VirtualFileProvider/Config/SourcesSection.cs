using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using Interfaces.Config;
using Interfaces.FileSystem;
using Interfaces.VirtualFile;
using VirtualFileProvider.Directory;
using VirtualFileProvider.XML;

namespace VirtualFileProvider.Config
{
    public class SourcesSection : ConfigurationSection , IEnumerator<IVirtualFileProviderFactory>
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
        public IVirtualFileProviderFactory Current
        {
            get
            {
                var prop = _enumerator.Current as ConfigurationProperty;
                var item = base[prop];
                return item as IVirtualFileProviderFactory;
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
