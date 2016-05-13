namespace DataAccess.Interfaces
{
    public interface IVirtualFileConfigurationReader
    {
        AvailableVirtualFileProviderDo ReadToDO();

        VirtualFileFactoryContext BuildContext(string selectedSource);

        IVirtualFileFactory FindActiveFactory(string activeProviderGrp);
    }
}
