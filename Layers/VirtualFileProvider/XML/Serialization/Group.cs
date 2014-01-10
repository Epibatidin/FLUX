using System;
using System.Xml.Serialization;
using MockUp.XMLItems;

namespace VirtualFileProvider.XML.Serialization
{
    [Serializable()]
    [XmlType("Group")]
    [XmlRoot(Namespace = "")]
    public class Group
    {
        [XmlElement]
        public Source Source { get; set; }

        [XmlElement]
        public LayerResults Results { get; set; } 
    }
}
