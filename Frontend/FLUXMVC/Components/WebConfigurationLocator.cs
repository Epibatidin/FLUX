using System.Configuration;
using System.Web;
using ConfigurationExtensions.Interfaces;

namespace FLUXMVC.Components
{
    public class WebConfigurationLocator : IConfigurationLocator
    {
        private readonly string _path;
        public WebConfigurationLocator(HttpContextBase context, string configName)
        {
            _path = context.Server.MapPath("/"+ configName+ ".config");
        }

        public Configuration Locate()
        {
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = _path;
            var config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

            if(config == null)
                throw new ConfigurationErrorsException("Cant find config " + _path);

            return config;
        }
    }
}