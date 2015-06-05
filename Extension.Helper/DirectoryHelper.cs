using System.IO;

namespace Extension.Helper
{
    public static class DirectoryHelper
    {
        public static string GetFolderName(this DirectoryInfo di)
        {
            var endpos = di.FullName.LastIndexOf('/');
            if (endpos >= 0)
                return di.FullName.Substring(endpos);
            return null;
        }
    }
}
