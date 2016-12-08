using System.Xml.Serialization;

namespace DataAccess.XMLStub.Serialization
{    
    public class MyKv
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}