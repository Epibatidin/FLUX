using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MockUp.XMLItems
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
