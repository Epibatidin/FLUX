using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using ConfigurationExtensions.Interfaces;
using Interfaces.Config;
using Interfaces.Frontend;
using VirtualFileProvider;
using VirtualFileProvider.Config;

namespace FLUXMVC.Components
{
    public class VirtualFileProviderSessionHandler : IVirtualFileProviderSessionHandler
    {
        public const string SessionDataKey = "VirtualFileProviderObjectKey";
        public const string SessionValueKey = "VirtualFileProviderSelected";

        private readonly HttpSessionStateBase _session;
        private readonly IVirtualFileFactory _providerFactory;

        public VirtualFileProviderSessionHandler(HttpSessionStateBase session, IConfigurationLocator locator)
            : this(session, new VirtualFileFactory(VirtualFileAccessorSectionGroup.Get(locator))) {}

        public VirtualFileProviderSessionHandler(HttpSessionStateBase session, IVirtualFileFactory factory)
        {
            _providerFactory = factory;
            _session = session;

            if (CurrentProviderKey == null)
                CurrentProviderKey = factory.DefaultProviderKey;
        }

        public IVirtualFileProvider GetVirtualFileProvider()
        {
            var obj = _session[SessionDataKey];

            if (obj == null)
            {
                obj = _providerFactory.GetProvider(CurrentProviderKey);
                _session[SessionDataKey] = obj;
            }
            return obj as IVirtualFileProvider;
        }
    
        public string CurrentProviderKey
        {
            get
            {
                return _session[SessionValueKey] as string;
            }
            set
            {
                _session[SessionValueKey] = value;
                _session[SessionDataKey] = null;
            }
        }

        public IEnumerable<SelectListItem> Providers
        {
            get
            {
                return new SelectList(_providerFactory.AvailableProviders, CurrentProviderKey);
            }
        }
    }
}