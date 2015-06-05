namespace DataAccess.Interfaces
{
    public interface IVirtualFileConfigurationReader
    {
        AvailableVirtualFileProviderDO ReadToDO();

        //string DefaultProviderKey { get; }
        //IEnumerable<string> AvailableProviders { get; }
        //IVirtualFileProvider GetProvider(string providerKey);
    }
}
