using System.Collections.Generic;
using System.Configuration;
using ConfigurationExtensions.Interfaces;
using Interfaces.Config;
using VirtualFileProvider.Config;

namespace VirtualFileProvider
{
    public class VirtualFileFactory : IVirtualFileFactory
    {
        private readonly VirtualFileAccessorSectionGroup _virtualFileAccessorSectionGroup;

        public string DefaultProviderKey { get; private set; }
        public IEnumerable<string> AvailableProviders { get; private set; }


        public VirtualFileFactory(VirtualFileAccessorSectionGroup virtualFileAccessorSectionGroup)
        {
            _virtualFileAccessorSectionGroup = virtualFileAccessorSectionGroup;
            DefaultProviderKey = _virtualFileAccessorSectionGroup.General.Active;
            var availProviders = new List<string>();
            var sources = _virtualFileAccessorSectionGroup.Sources;
            sources.Reset();
            while (sources.MoveNext())
            {
                var cur = sources.Current as ConfigurationElementCollection;

                if(cur == null) continue;
                var enumer = cur.GetEnumerator();
                while (enumer.MoveNext())
                {
                    var item = enumer.Current as IKeyedElement;
                    availProviders.Add(item.Key);
                }
            }
            AvailableProviders = availProviders;
        }


        public IVirtualFileProvider GetProvider(string providerKey)
        {
            var sources = _virtualFileAccessorSectionGroup.Sources;
            sources.Reset();
            while (sources.MoveNext())
            {
                var cur = sources.Current;
                var provider = cur.Create(providerKey);
                if(provider == null) continue;
                var debug = _virtualFileAccessorSectionGroup.Debug;

                provider.Init(debug.RootNames, debug.SubRootPos);
                return provider;
            }
            
            return null;
        }
    }
}
