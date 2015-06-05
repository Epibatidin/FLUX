using System.Collections.Generic;
using DataAccess.Base.Config;
using DataAccess.Interfaces;

namespace DataAccess.Base
{
    public class VirtualFileConfigurationReader : IVirtualFileConfigurationReader
    {
        private readonly IVirtualFileAccessorSectionGroupProvider _sectionGroupProvider;

        public VirtualFileConfigurationReader(IVirtualFileAccessorSectionGroupProvider sectionGroupProvider)
        {
            _sectionGroupProvider = sectionGroupProvider;
        }

        public AvailableVirtualFileProviderDO ReadToDO()
        {
            var section = _sectionGroupProvider.VirtualFileAccessorConfig;

            var result = new AvailableVirtualFileProviderDO
            {
                CurrentProviderName = section.General.Active
            };
            
            var availProviders = new List<string>();
            var sources = section.Sources;
            sources.Reset();

            while (sources.MoveNext())
            {
                availProviders.Add(sources.Current);
            }
            result.VirtualFileProviderNames = availProviders;
            return result;
        }




        //var availProviders = new List<string>();
        //var sources = _virtualFileAccessorSectionGroup.Sources;
        //sources.Reset();
        //while (sources.MoveNext())
        //{
        //    var cur = sources.Current as ConfigurationElementCollection;

        //    if(cur == null) continue;
        //    var enumer = cur.GetEnumerator();
        //    while (enumer.MoveNext())
        //    {
        //        var item = enumer.Current as IKeyedElement;
        //        availProviders.Add(item.Key);
        //    }
        //}
        //AvailableProviders = availProviders;

        //public VirtualFileConfigurationReader(VirtualFileAccessorSectionGroup virtualFileAccessorSectionGroup)
        //{
        //    _virtualFileAccessorSectionGroup = virtualFileAccessorSectionGroup;
        //    DefaultProviderKey = _virtualFileAccessorSectionGroup.General.Active;
        //    var availProviders = new List<string>();
        //    var sources = _virtualFileAccessorSectionGroup.Sources;
        //    sources.Reset();
        //    while (sources.MoveNext())
        //    {
        //        var cur = sources.Current as ConfigurationElementCollection;

        //        if(cur == null) continue;
        //        var enumer = cur.GetEnumerator();
        //        while (enumer.MoveNext())
        //        {
        //            var item = enumer.Current as IKeyedElement;
        //            availProviders.Add(item.Key);
        //        }
        //    }
        //    AvailableProviders = availProviders;
        //}


        public IVirtualFileProvider GetProvider(string providerKey)
        {
            //var sources = _virtualFileAccessorSectionGroup.Sources;
            //sources.Reset();
            //while (sources.MoveNext())
            //{
            //    var cur = sources.Current;
            //    var provider = cur.Create(providerKey);
            //    if(provider == null) continue;
            //    var debug = _virtualFileAccessorSectionGroup.Debug;

            //    provider.Init(debug.RootNames, debug.SubRootPos);
            //    return provider;
            //}

            return null;
        }
    }

}
