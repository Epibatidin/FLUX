using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Helper
{
    public static class DirectoryHelper
    {
        public static string getFolderName(this DirectoryInfo di)
        {
            var endpos = di.FullName.LastIndexOf('/');
            if (endpos >= 0)
                return di.FullName.Substring(endpos);
            return null;
        }
    }
}
