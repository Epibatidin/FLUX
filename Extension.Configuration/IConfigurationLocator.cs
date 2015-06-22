
namespace Extension.Configuration
{
    public interface IConfigurationLocator
    {
        System.Configuration.Configuration Locate(string fileName);
    }
}
