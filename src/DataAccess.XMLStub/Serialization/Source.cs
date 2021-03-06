﻿using System.Collections.Generic;
using System.Xml.Serialization;

namespace DataAccess.XMLStub.Serialization
{
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
