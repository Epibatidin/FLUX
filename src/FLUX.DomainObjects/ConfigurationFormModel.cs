
using System.Collections.Generic;
using DataAccess.Interfaces;

namespace FLUX.DomainObjects
{
    public class ConfigurationFormModel
    {
        public AvailableVirtualFileProviderFormModel VirtualFileProvider { get; set; }

        public IDictionary<int, IVirtualFile> Files { get; set; }
    }
}