namespace Interfaces.Config
{
    public interface IVirtualFileProviderFactory : IKeyCollection
    {
        IVirtualFileProvider Create(string sourceKey);
    }
}
