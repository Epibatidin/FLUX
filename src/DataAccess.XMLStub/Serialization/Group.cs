﻿using System.Xml.Serialization;

namespace DataAccess.XMLStub.Serialization
{
    [XmlType("Group")]
    [XmlRoot(Namespace = "")]
    public class Group
    {
        [XmlElement]
        public Source Source { get; set; }

        //[XmlElement]
        //public LayerResults Results { get; set; }
    }
}
