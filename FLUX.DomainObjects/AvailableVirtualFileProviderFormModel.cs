using System.Collections.Generic;
using DataAccess.Interfaces;

namespace FLUX.DomainObjects
{
    public class AvailableVirtualFileProviderFormModel
    {
        public string CurrentProviderName { get; set; }

        public IList<ProviderGroupDO> ProviderNames { get; set; }

        public bool IsCurrentProviderSelected(string providerName)
        {
            return CurrentProviderName == providerName;
        }
    }
}
