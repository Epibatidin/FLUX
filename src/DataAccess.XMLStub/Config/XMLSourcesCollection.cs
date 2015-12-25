//using System.Collections.Generic;
//using System.Configuration;
//using Facade.Configuration;

//namespace DataAccess.XMLStub.Config
//{
//    [ConfigurationCollection(typeof(XMLSource), AddItemName = "XMLSource")]
//    public class XMLSourcesCollection : GenericElementCollection<XMLSource>
//    {
//        private List<string> _keys;
//        public IEnumerable<string> Keys
//        {
//            get
//            {
//                if (_keys == null)
//                {
//                    _keys = new List<string>();
//                    for (int i = 0; i < Count; i++)
//                    {
//                        _keys.Add(Item(i).Key);
//                    }
//                }
//                return _keys;
//            }
//        }


//        //public IVirtualFileProvider Create(string sourceKey)
//        //{
//        //    XMLVirtualFileProvider xml = null;

//        //    var item = Item(sourceKey);
//        //    if (item != null)
//        //    {
//        //        xml = new XMLVirtualFileProvider();
//        //        xml.Setup(new RealDirectory(item.XMLFolder));
//        //    }
//        //    return xml;
//        //}
//    }
//}
