//using System.Configuration;
//using System.Web.Hosting;
//using Facade.Configuration;

//namespace FLUX.Configuration.Config
//{
//    public class WebConfigurationLocator : IConfigurationLocator
//    {
//        public System.Configuration.Configuration Locate(string configfileName)
//        {
//            var exePath = HostingEnvironment.MapPath("/" + configfileName + ".config");

//            ExeConfigurationFileMap map = new ExeConfigurationFileMap
//            {
//                ExeConfigFilename = exePath
//            };

//            var config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

//            if(config == null || !config.HasFile)
//                throw new ConfigurationErrorsException("Cant find config " + exePath);

//            return config;
//        }
//    }
//}