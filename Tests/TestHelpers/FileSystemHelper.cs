
using System;
using System.Collections.Generic;
using Interfaces.FileSystem;
using Moq;

namespace TestHelpers
{
    public static class FileSystemHelper
    {
        public static string Pattern { get; set; }

        public static IVirtualDirectory CreateDirectory(int pos)
        {
            return CreateDirectory(String.Format(Pattern, pos));
        }

        public static IVirtualDirectory CreateDirectory(string name)
        {
            var vd = new Mock<IVirtualDirectory>();
            vd.Setup(c => c.DirectoryName).Returns(name);
            return vd.Object;
        }

        public static List<IVirtualDirectory> CreateDirList(int count)
        {
            List<IVirtualDirectory> _result = new List<IVirtualDirectory>();
            for (int i = 0; i < count; i++)
            {
                _result.Add(CreateDirectory(i));
            }
            return _result;
        }
    }
}
