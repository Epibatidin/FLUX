using System.Configuration;
using System.Web;
using ConfigurationExtensions.Interfaces;

namespace FLUXMVC.Components
{
    public class WebConfigurationLocator : IConfigurationLocator
    {
        private readonly string _path;
        public WebConfigurationLocator(HttpContextBase context)
        {
            _path = context.Server.MapPath("/VirtualFileProvider.config");
        }

        public Configuration Locate()
        {
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = _path;
            var config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            return config;
        }
    }
}