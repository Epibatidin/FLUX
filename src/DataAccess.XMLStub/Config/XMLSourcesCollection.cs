using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;

namespace DataAccess.XMLStub.Config
{
    public class XMLSourcesCollection : IVirtualFileRootConfiguration
    {
        public string ID { get; set; }

        public List<XMLSource> XmlSources { get; set; }

        public IEnumerable<string> Keys => XmlSources.Select(c => c.Name);

        //public IVirtualFileProvider Create(string sourceKey)
        //{
        //    XMLVirtualFileProvider xml = null;

        //    var item = Item(sourceKey);
        //    if (item != null)
        //    {
        //        xml = new XMLVirtualFileProvider();
        //        xml.Setup(new RealDirectory(item.XMLFolder));
        //    }
        //    return xml;
        //}
    }
}
