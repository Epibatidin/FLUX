using System.Collections.Generic;
using DataAccess.Interfaces;

namespace FLUX.DomainObjects
{
    public class AvailableVirtualFileProviderFormModel
    {
        public string CurrentProviderName { get; set; }
        
        public IList<ProviderGroupDo> ProviderNames { get; set; }

        public bool IsSelected(string providerName)
        {
            return CurrentProviderName == providerName;
        }
    }
}
