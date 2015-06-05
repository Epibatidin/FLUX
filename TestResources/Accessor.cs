using System.IO;
using System.Reflection;

namespace TestResources
{
    public static class Accessor
    {

        public static Stream GetStream(string filePath)
        {
            var asTypeName = filePath.Replace('/', '.');

            var stream = Assembly.GetAssembly(typeof(Accessor)).GetManifestResourceStream(asTypeName);
            return stream;
        }
    }
}
