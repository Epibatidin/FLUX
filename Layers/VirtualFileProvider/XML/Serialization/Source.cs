using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using VirtualFileProvider.XML.Serialization;

namespace MockUp.XMLItems
{
    [Serializable()]
    [XmlType("Source")]
    [XmlRoot(Namespace = "")]
    public class Source
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlElement("Item")]
        public List<SourceItem> Items { get; set; }
    }
}
