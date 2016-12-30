using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataAccess.FileSystem
{
    public class RealFileStreamWriter 
    {
        private DirectoryInfo _root;

        public RealFileStreamWriter()
        {
            _root = new DirectoryInfo(@"D:\FluxWorkBenchFiles\Working\Result");
        }


        public Stream OpenForWriteAccess(IList<string> relativePathParts, string extension)
        {
            var movingDir = _root;

            for (int i = 0; i < relativePathParts.Count - 1; i++)
            {
                var dirs = movingDir.GetDirectories(relativePathParts[i]);
                if (!dirs.Any())
                    movingDir = movingDir.CreateSubdirectory(relativePathParts[i]);
                else
                    movingDir = dirs[0];
            }

            string fullPath = movingDir.FullName + "\\" + relativePathParts[relativePathParts.Count - 1] + "." + extension;

            var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);

            return fileStream;

        }
    }
}
