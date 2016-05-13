using System.Xml.Serialization;

namespace DataAccess.XMLStub.Serialization
{
    [XmlRoot(Namespace = "")]
    public class Root
    {
        [XmlElement]
        public string RootPath { get; set; }

        [XmlElement]
        public string Name { get; set; }

        [XmlElement]
        public int Groups { get; set; }
    }
}
