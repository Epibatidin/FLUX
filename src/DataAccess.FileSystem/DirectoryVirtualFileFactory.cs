using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.FileSystem.Config;
using DataAccess.Interfaces;

namespace DataAccess.FileSystem
{
    public class DirectoryVirtualFileFactory : IVirtualFileFactory
    {
        private readonly DirectorySourcesCollection _config;
        public DirectoryVirtualFileFactory(DirectorySourcesCollection config)
        {
            _config = config;
        }

        public Type GetVirtualFileArrayType() => typeof(RealFile[]);

        public bool CanHandleProviderKey(string providerId) => _config.SectionName == providerId;

        public IVirtualFileStreamReader GetReader(VirtualFileFactoryContext context)
        {
            var path = getRootPath(context.SelectedSource);
            return new RealFileStreamReader(path);
        }

        private string getRootPath(string selectedSource)
        {
            var folder = _config.Folder.First(c => c.Key == selectedSource);
            var rootPath = _config.Root + "\\" + folder.SubFolder + "\\Origin\\";
            return rootPath;
        }

        public IList<IVirtualFile> RetrieveVirtualFiles(VirtualFileFactoryContext context)
        {
            var virtualFiles = new List<IVirtualFile>();

            var rootPath = getRootPath(context.SelectedSource);
            var pathDataHelper = new PathDataHelper();

            var root = new RealDirectory(rootPath);
            int id = 0;
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
            int currentId = 0;
            foreach (var subdir in dirs.OrderBy(c => c.DirectoryName))
            {
                pos = -1;
                arrayPos = 0;
                foreach (var subDirectory in subdir.GetDirectories())
                {
                    pos++;
                    if (subroots.Length > 0)
                    {
                        if (arrayPos >= subroots.Length)
                            continue;

                        if (subroots[arrayPos] != pos)
                            continue;

                        arrayPos++;
                    }
                    foreach (string virtualFile in subDirectory.GetFiles("*.mp3", true))
                    {
                        var realFile = new RealFile();
                        realFile.ID = currentId++;

                        var pathData = pathDataHelper.FullPathToVirtualPathData(virtualFile, rootPath);
                        realFile.PathParts = pathData.PathParts;
                        realFile.Extension = pathData.Extension;
                        
                        virtualFiles.Add(realFile);
                    }
                }
            }

            return virtualFiles;
        }
    }
}

