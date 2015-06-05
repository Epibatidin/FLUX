using System.Configuration;
using FLUX.Interfaces.Configuration;

namespace Extension.Test
{
    public class StaticConfigurationLocator : IConfigurationLocator
    {
        private readonly string _fullFilePath;

        public StaticConfigurationLocator(string fullFilePath)
        {
            _fullFilePath = fullFilePath;
        }

        public Configuration Locate(string fileName)
        {
            ExeConfigurationFileMap map = new ExeConfigurationFileMap
            {
                ExeConfigFilename = _fullFilePath
            };

            var config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

            if (config == null)
                throw new ConfigurationErrorsException("Cant find config " + _fullFilePath);
            return config;
        }
    }
}
