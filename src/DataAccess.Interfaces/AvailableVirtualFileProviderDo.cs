using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public class AvailableVirtualFileProviderDo
    {
        public AvailableVirtualFileProviderDo()
        {
            ProviderNames = new List<ProviderGroupDo>();
        }

        public string CurrentProviderName { get; set; }

        public IList<ProviderGroupDo> ProviderNames { get; private set; }
    }
}
