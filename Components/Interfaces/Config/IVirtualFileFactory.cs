using System.Collections.Generic;

namespace Interfaces.Config
{
    public interface IVirtualFileFactory
    {
        string DefaultProviderKey { get; }
        IEnumerable<string> AvailableProviders { get; }
        IVirtualFileProvider GetProvider(string providerKey);
    }
}
