using System.Collections.Generic;
using System.Web.Mvc;
using Interfaces.Config;

namespace Interfaces.Frontend
{
    public interface IVirtualFileProviderSessionHandler
    {
        string CurrentProviderKey { get; set; }
        IEnumerable<SelectListItem> Providers { get; }
        IVirtualFileProvider GetVirtualFileProvider();
    }
}
