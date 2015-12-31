//using System.Collections.Generic;
//using System.Configuration;
//using Extension.Configuration;

//namespace Extraction.Layer.File.Config
//{
//    public class BlackListConfig : ConfigurationElement , IBlackListConfig
//    {
//        private HashSet<string> _blacklist;
        
//        public HashSet<string> BlackList 
//        {
//            get
//            {
//                if (_blacklist == null)
//                {
//                    _blacklist = ConfigHelper.CommaSeparatedStringToHashSet(items);
//                }
//                return _blacklist;
//            }       
//        }

//        [ConfigurationProperty("Items", IsRequired = true)]
//        private string items
//        {
//            get
//            {
//                return (string)base["Items"];
//            }
//        }


//        [ConfigurationProperty("RepairCurses")]
//        public bool RepairCurses
//        {
//            get
//            {
//                return (bool)base["RepairCurses"];
//            }
//        }
//    }
//}
