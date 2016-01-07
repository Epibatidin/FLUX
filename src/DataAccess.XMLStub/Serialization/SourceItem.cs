using System.Xml.Serialization;
using DataAccess.Interfaces;

namespace DataAccess.XMLStub.Serialization
{
    [XmlType("Item")]
    public class SourceItem : IVirtualFile
    {
        [XmlAttribute()]
        public int ID { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlElement("URL")]
        public string VirtualPath { get; set; }

        [XmlElement("TagData")]
        public TagData TagData { get; set; }
    }
}
