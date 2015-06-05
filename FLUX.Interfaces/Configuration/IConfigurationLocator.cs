namespace FLUX.Interfaces.Configuration
{
    public interface IConfigurationLocator
    {
        System.Configuration.Configuration Locate(string fileName);
    }
}
