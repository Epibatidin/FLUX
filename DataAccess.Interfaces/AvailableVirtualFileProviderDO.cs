using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public class AvailableVirtualFileProviderDO
    {
        public string CurrentProviderName { get; set; }

        public IList<string> VirtualFileProviderNames { get; set; }
    }
}
