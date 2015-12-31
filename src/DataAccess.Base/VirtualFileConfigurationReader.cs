using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Base.Config;
using DataAccess.Interfaces;
using Microsoft.Extensions.OptionsModel;

namespace DataAccess.Base
{
    public class VirtualFileConfigurationReader : IVirtualFileConfigurationReader
    {
        private readonly IOptions<VirtualFileAccessorSectionGroup> _sectionGroupAccessor;
        private readonly IEnumerable<IVirtualFileRootConfiguration> _virtualFileRootConfigurations;
        private readonly IEnumerable<IVirtualFileFactory> _providerFactories;

        public VirtualFileConfigurationReader(IOptions<VirtualFileAccessorSectionGroup> sectionGroupAccessor, 
            IEnumerable<IVirtualFileRootConfiguration> virtualFileRootConfigurations, IEnumerable<IVirtualFileFactory> providerFactories)
        {
            _sectionGroupAccessor = sectionGroupAccessor;
            _virtualFileRootConfigurations = virtualFileRootConfigurations;
            _providerFactories = providerFactories;
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
                var providergroup = new ProviderGroupDo(virtualFileRootConfiguration.SectionName);

                foreach (var key in virtualFileRootConfiguration.Keys)
                {
                    providergroup.VirtualFileProviderNames.Add(key);
                }
                result.ProviderNames.Add(providergroup);
            }
            return result;
        }

        public IDictionary<int, IVirtualFile> GetVirtualFiles(string selectedSource, string activeProviderGrp)
        {
            var activeFactory = _providerFactories.FirstOrDefault(c => c.CanHandleProviderKey(activeProviderGrp));
            if(activeFactory == null)
                throw new NotSupportedException(string.Format("ProviderKey : {0} is not supported by any ProviderFactory", activeProviderGrp));

            var debugConfig = _sectionGroupAccessor.Value.Debug;

            var providerContext = new VirtualFileFactoryContext();
            providerContext.OverrideRootnames = debugConfig.RootNames;
            providerContext.SubRoots = debugConfig.SubRootPos;
            providerContext.SelectedSource = selectedSource;
            return activeFactory.RetrieveVirtualFiles(providerContext);
        }
    }
}
