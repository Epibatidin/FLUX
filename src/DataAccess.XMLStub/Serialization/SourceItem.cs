using System.Xml.Serialization;
using DataAccess.Interfaces;

namespace DataAccess.XMLStub.Serialization
{
    [XmlType("Item")]
    public class SourceItem : IVirtualFile
    {
        [XmlIgnore]
        public string Extension { get; set; }

        [XmlIgnore]
        public string Name { get; set; }

        [XmlIgnore]
        public string[] PathParts { get; set; }

        [XmlAttribute()]
        public int ID { get; set; }        

        [XmlElement("URL")]
        public string VirtualPath { get; set; }

        [XmlElement("TagData")]
        public TagData TagData { get; set; }

        [XmlElement("Result")]
        public ResultContainer Results { get; set; }

    }
}
