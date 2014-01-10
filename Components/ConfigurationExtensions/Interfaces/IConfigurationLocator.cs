using System.Configuration;

namespace ConfigurationExtensions.Interfaces
{
    public interface IConfigurationLocator
    {
        Configuration Locate();
    }
}
