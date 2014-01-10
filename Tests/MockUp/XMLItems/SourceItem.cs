﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Interfaces;
using System.IO;
using Helper;

namespace MockUp.XMLItems
{
    [Serializable]   
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

        public Stream Open()
        {
            TagData.End = ByteHelper.StringToByte("TAG" + new string(' ', 125));
            return new TagFakeStream(TagData);            
        }
    }
}
