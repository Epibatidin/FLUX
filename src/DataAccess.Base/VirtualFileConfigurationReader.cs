using System.Collections.Generic;
using DataAccess.Base.Config;
using DataAccess.Interfaces;
using Microsoft.Extensions.OptionsModel;

namespace DataAccess.Base
{
    public class VirtualFileConfigurationReader : IVirtualFileConfigurationReader
    {
        private readonly IOptions<VirtualFileAccessorSectionGroup> _sectionGroupAccessor;
        private readonly IEnumerable<IVirtualFileRootConfiguration> _virtualFileRootConfigurations;

        public VirtualFileConfigurationReader(IOptions<VirtualFileAccessorSectionGroup> sectionGroupAccessor, 
            IEnumerable<IVirtualFileRootConfiguration> virtualFileRootConfigurations )
        {
            _sectionGroupAccessor = sectionGroupAccessor;
            _virtualFileRootConfigurations = virtualFileRootConfigurations;
        }

        public AvailableVirtualFileProviderDo ReadToDO()
        {
            var config = _sectionGroupAccessor.Value;

            var result = new AvailableVirtualFileProviderDo
            {
                CurrentProviderName = config.General.Active
            };

            foreach (var virtualFileRootConfiguration in _virtualFileRootConfigurations)
            {
                var providergroup = new ProviderGroupDo();
                providergroup.GroupName = virtualFileRootConfiguration.Root;

                foreach (var key in virtualFileRootConfiguration.Keys)
                {
                    providergroup.VirtualFileProviderNames.Add(key);
                }
                result.ProviderNames.Add(providergroup);
            }
            return result;
        }

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
