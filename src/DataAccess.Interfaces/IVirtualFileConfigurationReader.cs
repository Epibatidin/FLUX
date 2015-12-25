namespace DataAccess.Interfaces
{
    public interface IVirtualFileConfigurationReader
    {
        AvailableVirtualFileProviderDo ReadToDO();
        
        //string DefaultProviderKey { get; }
        //IEnumerable<string> AvailableProviders { get; }
        //IVirtualFileProvider GetProvider(string providerKey);
    }
}
