using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public class ProviderGroupDo
    {
        public ProviderGroupDo(string providerKey)
        {
            ProviderKey = providerKey;
            VirtualFileProviderNames = new List<string>();
        }

        public string ProviderKey { get; private set; }

        public IList<string> VirtualFileProviderNames { get; private set; }
    }
}