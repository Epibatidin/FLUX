using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MockUp.XMLItems
{
    public class TagData
    {
        public long ContentLength { get; set; }

        public byte[] Begin { get; set; }
        public byte[] End { get; set; }
    }
}
