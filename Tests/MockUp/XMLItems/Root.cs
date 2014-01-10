using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace MockUp.XMLItems
{
    [XmlRoot(Namespace="")]
    public class Root
    {
        [XmlElement]
        public int RootPathLength { get; set; }

        [XmlElement]
        public string Name { get; set; }

        [XmlElement]
        public int Groups { get; set; }
    }
}
