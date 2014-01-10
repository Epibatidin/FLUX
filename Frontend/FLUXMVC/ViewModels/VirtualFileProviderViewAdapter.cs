using System.Collections.Generic;
using Interfaces.Config;
using VirtualFileProvider;
using VirtualFileProvider.Config;

namespace FLUXMVC.ViewModels
{
    public class VirtualFileProviderViewAdapter
    {
        public IVirtualFileFactory Factory { get; private set; }
        public IEnumerable<string> Providers { get; private set; }
        public string DefaultProvider { get; private set; }


        public VirtualFileProviderViewAdapter(VirtualFileAccessorSectionGroup config, IVirtualFileFactory factory)
        {
            _factory = factory;
            Providers = _factory.AvailableProviders;
            DefaultProvider = config.General.Active;
        }


        public VirtualFileProviderViewAdapter(IConfigurationLocator configLocator)
        {
            var config = VirtualFileAccessorSectionGroup.Get(configLocator);
            _factory = new VirtualFileFactory(config);

        }

        //}

        //public IVirtualFileProvider GetProvider(string provider)
        //{
        //    // NO Update 
        //    // just stateless !

        //    return _factory.GetProvider(provider);
        //}
    }
}