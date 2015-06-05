//using System.Configuration;
//using System.IO;
//using ConfigurationExtensions.Interfaces;

//namespace TestHelpers
//{
//    public class StaticConfigurationLocator : IConfigurationLocator
//    {
//        private string _filePath;

//        public StaticConfigurationLocator(string fileName)
//            : this(@"E:\Develop\FLUX\Tests\TestHelpers\Config\", fileName)
//        {
//        }

//        public StaticConfigurationLocator(string rootDir, string fileName)
//        {
//            _filePath = Path.Combine(rootDir, fileName + ".config");
//        }
        
//        public Configuration Locate()
//        {
//            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
//            map.ExeConfigFilename = _filePath;
//            var config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
//            return config;
//        }
//    }
//}
