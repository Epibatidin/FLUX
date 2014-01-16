using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Interfaces.VirtualFile;

namespace TestHelpers
{
    public class VirtualFileDummy :IVirtualFile
    {
        private int _id;
        private string _name;
        private string _virtualPath;

        public int ID { get; set; }

        public string Name { get; set; }

        public string VirtualPath { get; set; }

        public Stream Open()
        {
            return new MemoryStream();
        }
    }
}
