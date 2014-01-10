using System.Xml.Serialization;

namespace VirtualFileProvider.XML.Serialization
{
    [XmlRoot(Namespace="")]
    public class Root
    {
        [XmlElement]
        public string Name { get; set; }

        [XmlElement]
        public int Groups { get; set; }
    }
}
