using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Interfaces.VirtualFile;

namespace VirtualFileProvider.Tests.Directory
{
    public class MockFile :IVirtualFile
    {
        public MockFile(int id)
        {
            ID = id;
        }


        public int ID { get; set; }

        public string Name { get; set; }

        public string VirtualPath { get; set; }

        public Stream Open()
        {
            return new MemoryStream();
        }
    }
}
