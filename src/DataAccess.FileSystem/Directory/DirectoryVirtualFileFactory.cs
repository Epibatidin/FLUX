using DataAccess.Interfaces;
using System.Collections.Generic;
using System.Linq;
using DataAccess.FileSystem.Config;

namespace DataAccess.FileSystem.Directory
{
    public class DirectoryVirtualFileFactory : IVirtualFileFactory
    {
        private readonly DirectorySourcesCollection _config;

        public DirectoryVirtualFileFactory(DirectorySourcesCollection config)
        {
            _config = config;
        }

        public bool CanHandleProviderKey(string providerId) => _config.ID == providerId;

        public IDictionary<int, IVirtualFile> RetrieveVirtualFiles(VirtualFileFactoryContext context)
        {
            var virtualFiles = new Dictionary<int, IVirtualFile>();

            var folder = _config.Folder.First(c => c.Key == context.SelectedSource);
            var rootPath = _config.Root + "\\" + folder.SubFolder + "\\Origin";
            var root = new RealDirectory(rootPath);

            var temp = root.GetDirectories();
            IEnumerable<IVirtualDirectory> dirs;
            if (context.OverrideRootnames == null)
                dirs = temp;
            else
            {
                dirs = from d in temp
                       join over in context.OverrideRootnames on d.DirectoryName equals over
                       select d;
            }
            int pos, arrayPos;
            var subroots = context.SubRoots ?? new int[0];

            foreach (var subdir in dirs.OrderBy(c => c.DirectoryName))
            {
                pos = -1;
                arrayPos = 0;
                foreach (var subDirectory in subdir.GetDirectories())
                {
                    pos++;
                    if (arrayPos >= subroots.Length)
                        continue;

                    if (subroots[arrayPos] != pos)
                        continue;

                    arrayPos++;
                    
                    foreach (var virtualFile in subDirectory.GetFiles("*.mp3", true, c => (pos + 1)*1000 + c))
                    {
                        virtualFiles.Add(virtualFile.ID, virtualFile);
                    }
                }
            }

            return virtualFiles;
        }
    }
}
