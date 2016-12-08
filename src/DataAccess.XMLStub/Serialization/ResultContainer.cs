using System.Collections.Generic;
using System.Xml.Serialization;

namespace DataAccess.XMLStub.Serialization
{
    public class ResultContainer
    {
        [XmlElement("KV")]
        public List<MyKv> KVs { get; set; }
    }
}
