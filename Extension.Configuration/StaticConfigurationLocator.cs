using System.Configuration;

namespace Extension.Configuration
{
    public class StaticConfigurationLocator : IConfigurationLocator
    {
        private readonly string _fullBaseFilePath;

        public StaticConfigurationLocator(string fullBaseFilePath)
        {
            _fullBaseFilePath = fullBaseFilePath;
        }

        public System.Configuration.Configuration Locate(string fileName)
        {
            var fullFileName = _fullBaseFilePath + "/" + fileName + ".config";
            ExeConfigurationFileMap map = new ExeConfigurationFileMap
            {
                ExeConfigFilename = fullFileName
            };

            var config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

            if (config == null || !config.HasFile)
                throw new ConfigurationErrorsException("Cant find config " + fullFileName);
            return config;
        }
    }
}
