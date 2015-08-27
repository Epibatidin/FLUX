using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public class AvailableVirtualFileProviderDO
    {
        public AvailableVirtualFileProviderDO()
        {
            ProviderNames = new List<ProviderGroupDO>();
        }

        public string CurrentProviderName { get; set; }

        public IList<ProviderGroupDO> ProviderNames { get; private set; }
    }

    public class ProviderGroupDO
    {
        public ProviderGroupDO()
        {
            VirtualFileProviderNames = new List<string>();
        }

        public string GroupName { get; set; }
        public IList<string> VirtualFileProviderNames { get; private set; }
    }
}
