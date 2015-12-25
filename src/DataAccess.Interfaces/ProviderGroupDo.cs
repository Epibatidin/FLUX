using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public class ProviderGroupDo
    {
        public ProviderGroupDo()
        {
            VirtualFileProviderNames = new List<string>();
        }

        public string GroupName { get; set; }
        public IList<string> VirtualFileProviderNames { get; private set; }
    }
}